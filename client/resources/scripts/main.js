// EzEats - Main JavaScript

// API Configuration
const API_BASE_URL = 'http://localhost:5258/api';

// Global data storage
let recipes = [];
let mealPlans = [];
let categories = [];

// DOM elements
const recipesContainer = document.getElementById('recipes-container');
const mealPlansContainer = document.getElementById('meal-plans-container');

// Current filter state
let currentFilter = 'all';
let showAllRecipes = false;

// API Functions
async function fetchRecipes() {
  try {
    const response = await fetch(`${API_BASE_URL}/Recipe`);
    if (!response.ok) throw new Error('Failed to fetch recipes');
    return await response.json();
  } catch (error) {
    console.error('Error fetching recipes:', error);
    return [];
  }
}

async function fetchMealPlans() {
  try {
    const response = await fetch(`${API_BASE_URL}/MealPlan`);
    if (!response.ok) throw new Error('Failed to fetch meal plans');
    return await response.json();
  } catch (error) {
    console.error('Error fetching meal plans:', error);
    return [];
  }
}

async function fetchCategories() {
  try {
    const response = await fetch(`${API_BASE_URL}/Category`);
    if (!response.ok) throw new Error('Failed to fetch categories');
    return await response.json();
  } catch (error) {
    console.error('Error fetching categories:', error);
    return [];
  }
}

// Initialize the app
document.addEventListener('DOMContentLoaded', async function() {
  try {
    // Load data from API
    recipes = await fetchRecipes();
    mealPlans = await fetchMealPlans();
    categories = await fetchCategories();
    
    // Render the UI
    renderRecipes();
    renderMealPlans();
    setupEventListeners();
  } catch (error) {
    console.error('Error initializing app:', error);
    // Fallback to empty arrays if API fails
    recipes = [];
    mealPlans = [];
    categories = [];
    renderRecipes();
    renderMealPlans();
    setupEventListeners();
  }
});

// Filter recipes by category
function filterRecipes(category) {
  currentFilter = category;
  showAllRecipes = false; // Reset show all when switching categories
  
  // Update active button
  document.querySelectorAll('[data-category]').forEach(btn => {
    btn.classList.remove('active');
  });
  document.querySelector(`[data-category="${category}"]`).classList.add('active');
  
  // Re-render recipes
  renderRecipes();
}

// Toggle show more recipes
function toggleShowMore() {
  showAllRecipes = !showAllRecipes;
  renderRecipes();
}

// Get category icon
function getCategoryIcon(category) {
  const icons = {
    'No-Cook': 'bi-snow',
    'Microwave': 'bi-lightning-charge',
    'Stovetop': 'bi-fire'
  };
  return icons[category] || 'bi-grid-3x3-gap';
}

// Get category color
function getCategoryColor(category) {
  const colors = {
    'No-Cook': 'info',
    'Microwave': 'warning',
    'Stovetop': 'danger'
  };
  return colors[category] || 'secondary';
}

// Render recipes
function renderRecipes() {
  if (!recipesContainer) return;
  
  // Filter recipes based on current filter
  const filteredRecipes = currentFilter === 'all' 
    ? recipes 
    : recipes.filter(recipe => recipe.category?.name === currentFilter);
  
  // Determine how many recipes to show
  let recipesToShow = filteredRecipes;
  let showMoreButton = false;
  
  // Only limit "All Recipes" view
  if (currentFilter === 'all' && !showAllRecipes) {
    recipesToShow = filteredRecipes.slice(0, 4);
    showMoreButton = filteredRecipes.length > 4;
  }
  
  // Render recipes
  recipesContainer.innerHTML = recipesToShow.map(recipe => `
    <div class="col-lg-3 col-md-6 mb-4">
      <div class="recipe-card h-100">
        <div class="recipe-image" style="background-image: url('${recipe.image}')">
          <div class="category-badge">
            <span class="badge bg-${getCategoryColor(recipe.category?.name || '')}">
              <i class="bi ${getCategoryIcon(recipe.category?.name || '')} me-1"></i>${recipe.category?.name || 'Unknown'}
            </span>
          </div>
        </div>
        <div class="p-3">
          <h5 class="fw-bold">${recipe.title}</h5>
          <p class="text-muted small">${recipe.description}</p>
          <div class="d-flex justify-content-between align-items-center mb-2">
            <small class="text-primary"><i class="bi bi-clock me-1"></i>${recipe.prepTime}</small>
            <small class="text-success"><i class="bi bi-check-circle me-1"></i>${recipe.difficulty}</small>
          </div>
          <div class="d-flex justify-content-between align-items-center mb-3">
            <span class="price-tag fw-bold text-success">${recipe.price}</span>
            <small class="text-muted">per serving</small>
          </div>
          <div class="d-grid gap-2">
            <button class="btn btn-outline-primary btn-sm" onclick="showRecipeDetails(${recipe.id})">
              View Recipe
            </button>
            <button class="btn btn-primary btn-sm" onclick="addToCart(${recipe.id})" id="add-to-cart-${recipe.id}">
              <i class="bi bi-cart-plus me-1"></i>Add to Cart
            </button>
          </div>
        </div>
      </div>
    </div>
  `).join('');
  
  // Show/hide the "Show More" button
  const showMoreContainer = document.getElementById('show-more-container');
  const showMoreBtn = document.getElementById('show-more-btn');
  
  if (showMoreContainer && showMoreBtn) {
    if (showMoreButton) {
      showMoreContainer.style.display = 'flex';
      showMoreBtn.innerHTML = showAllRecipes 
        ? '<i class="bi bi-chevron-up me-2"></i>Show Less Recipes'
        : '<i class="bi bi-chevron-down me-2"></i>Show More Recipes';
    } else {
      showMoreContainer.style.display = 'none';
    }
  }
}

// Render meal plans
function renderMealPlans() {
  if (!mealPlansContainer) return;
  
  mealPlansContainer.innerHTML = mealPlans.map(plan => `
    <div class="col-lg-4 col-md-6 mb-4">
      <div class="meal-plan-card h-100">
        <div class="p-4">
          <h5 class="fw-bold text-primary">${plan.title}</h5>
          <p class="text-muted">${plan.description}</p>
          <div class="mb-3">
            <strong>Price Range:</strong> ${plan.price}<br>
            <strong>Duration:</strong> ${plan.duration}
          </div>
          <div class="mb-3">
            <strong>Includes:</strong>
            <ul class="list-unstyled mt-2">
              ${plan.mealPlanRecipes?.map(mpr => `<li><i class="bi bi-check text-success me-2"></i>${mpr.recipe?.title || 'Unknown Recipe'}</li>`).join('') || '<li>No recipes available</li>'}
            </ul>
          </div>
          <div class="d-grid gap-2">
            <button class="btn btn-primary" onclick="addMealPlanToCart(${plan.id})" id="add-meal-plan-${plan.id}">
              <i class="bi bi-cart-plus me-1"></i>Add to Cart
            </button>
            <button class="btn btn-outline-primary btn-sm" onclick="selectMealPlan(${plan.id})">
              View Details
            </button>
          </div>
        </div>
      </div>
    </div>
  `).join('');
}

// Show recipe details in a modal
function showRecipeDetails(recipeId) {
  const recipe = recipes.find(r => r.id === recipeId);
  if (!recipe) return;

  const modal = new bootstrap.Modal(document.getElementById('recipeModal') || createRecipeModal());
  
  // Update modal content
  document.getElementById('recipeTitle').textContent = recipe.title;
  document.getElementById('recipeImage').style.backgroundImage = `url('${recipe.image}')`;
  document.getElementById('recipeDescription').textContent = recipe.description;
  document.getElementById('recipePrepTime').textContent = recipe.prepTime;
  document.getElementById('recipeDifficulty').textContent = recipe.difficulty;
  
  // Update ingredients
  const ingredientsList = document.getElementById('recipeIngredients');
  ingredientsList.innerHTML = recipe.ingredients?.map(ingredient => 
    `<li class="list-group-item">${ingredient.name}</li>`
  ).join('') || '<li class="list-group-item">No ingredients available</li>';
  
  // Update instructions
  const instructionsList = document.getElementById('recipeInstructions');
  instructionsList.innerHTML = recipe.instructions?.map((instruction, index) => 
    `<li class="list-group-item"><strong>Step ${instruction.stepNumber}:</strong> ${instruction.instructionText}</li>`
  ).join('') || '<li class="list-group-item">No instructions available</li>';
  
  modal.show();
}

// Create recipe modal if it doesn't exist
function createRecipeModal() {
  const modalHTML = `
    <div class="modal fade" id="recipeModal" tabindex="-1">
      <div class="modal-dialog modal-lg">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="recipeTitle">Recipe Details</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
          </div>
          <div class="modal-body">
            <div class="row">
              <div class="col-md-6">
                <div id="recipeImage" class="recipe-image rounded mb-3"></div>
                <p id="recipeDescription" class="text-muted"></p>
                <div class="d-flex gap-3 mb-3">
                  <small class="text-primary"><i class="bi bi-clock me-1"></i><span id="recipePrepTime"></span></small>
                  <small class="text-success"><i class="bi bi-check-circle me-1"></i><span id="recipeDifficulty"></span></small>
                </div>
              </div>
              <div class="col-md-6">
                <h6>Ingredients:</h6>
                <ul id="recipeIngredients" class="list-group list-group-flush mb-3"></ul>
                
                <h6>Instructions:</h6>
                <ol id="recipeInstructions" class="list-group list-group-flush"></ol>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Save Recipe</button>
          </div>
        </div>
      </div>
    </div>
  `;
  
  document.body.insertAdjacentHTML('beforeend', modalHTML);
  return document.getElementById('recipeModal');
}

// Select meal plan
function selectMealPlan(planId) {
  const plan = mealPlans.find(p => p.id === planId);
  if (!plan) return;
  
  const recipes = plan.mealPlanRecipes?.map(mpr => mpr.recipe?.title || 'Unknown Recipe').join(', ') || 'No recipes available';
  
  alert(`Meal Plan: ${plan.title}\n\nDescription: ${plan.description}\n\nPrice Range: ${plan.price}\nDuration: ${plan.duration}\n\nRecipes included:\n${recipes}\n\nClick "Add to Cart" to add all recipes to your cart!`);
}

// Setup event listeners
function setupEventListeners() {
  // Smooth scrolling for navigation links
  document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
      e.preventDefault();
      const target = document.querySelector(this.getAttribute('href'));
      if (target) {
        target.scrollIntoView({
          behavior: 'smooth',
          block: 'start'
        });
      }
    });
  });
}

// Utility functions
function formatTime(minutes) {
  return `${minutes} min`;
}

function getDifficultyColor(difficulty) {
  const colors = {
    'Easy': 'success',
    'Medium': 'warning',
    'Hard': 'danger'
  };
  return colors[difficulty] || 'secondary';
}

// Authentication and Cart Management
let currentUser = null;
let authToken = null;
let cartItems = [];

// Initialize authentication
function initAuth() {
  authToken = localStorage.getItem('authToken');
  if (authToken) {
    fetchCurrentUser();
  }
}

// Show login modal
function showLoginModal() {
  const modal = new bootstrap.Modal(document.getElementById('loginModal'));
  modal.show();
}

// Show register modal
function showRegisterModal() {
  const modal = new bootstrap.Modal(document.getElementById('registerModal'));
  modal.show();
}

// Login function
async function login(email, password) {
  try {
    const response = await fetch(`${API_BASE_URL}/Auth/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ email, password })
    });

    if (!response.ok) {
      throw new Error('Login failed');
    }

    const data = await response.json();
    authToken = data.token;
    currentUser = data.user;
    
    localStorage.setItem('authToken', authToken);
    localStorage.setItem('currentUser', JSON.stringify(currentUser));
    
    updateUIForLoggedInUser();
    
    // Close modal
    const modal = bootstrap.Modal.getInstance(document.getElementById('loginModal'));
    modal.hide();
    
    // Load cart
    await loadCart();
    
    return true;
  } catch (error) {
    console.error('Login error:', error);
    alert('Login failed. Please check your credentials.');
    return false;
  }
}

// Logout function
function logout() {
  authToken = null;
  currentUser = null;
  cartItems = [];
  
  localStorage.removeItem('authToken');
  localStorage.removeItem('currentUser');
  
  updateUIForLoggedOutUser();
}

// Update UI for logged in user
function updateUIForLoggedInUser() {
  document.querySelectorAll('.nav-item button').forEach(btn => {
    if (btn.textContent.includes('Login') || btn.textContent.includes('Register')) {
      btn.parentElement.classList.add('d-none');
    }
  });
  
  document.getElementById('user-menu').classList.remove('d-none');
  document.getElementById('user-name').textContent = `${currentUser.firstName} ${currentUser.lastName}`;
}

// Update UI for logged out user
function updateUIForLoggedOutUser() {
  document.querySelectorAll('.nav-item button').forEach(btn => {
    if (btn.textContent.includes('Login') || btn.textContent.includes('Register')) {
      btn.parentElement.classList.remove('d-none');
    }
  });
  
  document.getElementById('user-menu').classList.add('d-none');
  document.getElementById('cart-count').textContent = '0';
}

// Register function
async function register(userData) {
  try {
    const response = await fetch(`${API_BASE_URL}/Auth/register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(userData)
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.message || 'Registration failed');
    }

    const data = await response.json();
    authToken = data.token;
    currentUser = data.user;
    
    localStorage.setItem('authToken', authToken);
    localStorage.setItem('currentUser', JSON.stringify(currentUser));
    
    updateUIForLoggedInUser();
    
    // Close modal
    const modal = bootstrap.Modal.getInstance(document.getElementById('registerModal'));
    modal.hide();
    
    // Load cart
    await loadCart();
    
    return true;
  } catch (error) {
    console.error('Registration error:', error);
    alert('Registration failed: ' + error.message);
    return false;
  }
}

// Fetch current user
async function fetchCurrentUser() {
  try {
    const response = await fetch(`${API_BASE_URL}/Auth/me`, {
      headers: {
        'Authorization': `Bearer ${authToken}`
      }
    });

    if (response.ok) {
      currentUser = await response.json();
      localStorage.setItem('currentUser', JSON.stringify(currentUser));
      updateUIForLoggedInUser();
      await loadCart();
    } else {
      logout();
    }
  } catch (error) {
    console.error('Error fetching user:', error);
    logout();
  }
}

// Add to cart
async function addToCart(recipeId) {
  if (!authToken) {
    showLoginModal();
    return;
  }

  try {
    const response = await fetch(`${API_BASE_URL}/Cart/add`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${authToken}`
      },
      body: JSON.stringify({ recipeId, quantity: 1 })
    });

    if (response.ok) {
      await loadCart();
      // Show success feedback
      const button = document.getElementById(`add-to-cart-${recipeId}`);
      const originalText = button.innerHTML;
      button.innerHTML = '<i class="bi bi-check me-1"></i>Added!';
      button.classList.remove('btn-primary');
      button.classList.add('btn-success');
      
      setTimeout(() => {
        button.innerHTML = originalText;
        button.classList.remove('btn-success');
        button.classList.add('btn-primary');
      }, 2000);
    } else {
      throw new Error('Failed to add to cart');
    }
  } catch (error) {
    console.error('Error adding to cart:', error);
    alert('Failed to add item to cart');
  }
}

// Load cart
async function loadCart() {
  if (!authToken) return;

  try {
    const response = await fetch(`${API_BASE_URL}/Cart`, {
      headers: {
        'Authorization': `Bearer ${authToken}`
      }
    });

    if (response.ok) {
      const cart = await response.json();
      cartItems = cart.items || [];
      document.getElementById('cart-count').textContent = cart.totalItems || 0;
    }
  } catch (error) {
    console.error('Error loading cart:', error);
  }
}

// Show cart
async function showCart() {
  if (!authToken) {
    showLoginModal();
    return;
  }

  await loadCart();
  
  const cartContent = document.getElementById('cart-content');
  
  if (cartItems.length === 0) {
    cartContent.innerHTML = '<p class="text-center text-muted">Your cart is empty</p>';
  } else {
    cartContent.innerHTML = cartItems.map(item => `
      <div class="card mb-3">
        <div class="card-body">
          <div class="row align-items-center">
            <div class="col-md-2">
              <div class="recipe-image-small" style="background-image: url('${item.recipeImage}'); height: 60px; background-size: cover; background-position: center; border-radius: 5px;"></div>
            </div>
            <div class="col-md-4">
              <h6 class="mb-1">${item.recipeTitle}</h6>
              <small class="text-muted">${item.categoryName}</small>
            </div>
            <div class="col-md-2">
              <span class="text-success fw-bold">${item.recipePrice}</span>
            </div>
            <div class="col-md-2">
              <div class="input-group input-group-sm">
                <button class="btn btn-outline-secondary" onclick="updateCartItem(${item.id}, ${item.quantity - 1})">-</button>
                <input type="number" class="form-control text-center" value="${item.quantity}" min="1" onchange="updateCartItem(${item.id}, this.value)">
                <button class="btn btn-outline-secondary" onclick="updateCartItem(${item.id}, ${item.quantity + 1})">+</button>
              </div>
            </div>
            <div class="col-md-2">
              <button class="btn btn-outline-danger btn-sm" onclick="removeFromCart(${item.id})">
                <i class="bi bi-trash"></i>
              </button>
            </div>
          </div>
        </div>
      </div>
    `).join('');
  }

  const modal = new bootstrap.Modal(document.getElementById('cartModal'));
  modal.show();
}

// Update cart item
async function updateCartItem(cartItemId, quantity) {
  if (!authToken) return;

  try {
    const response = await fetch(`${API_BASE_URL}/Cart/update`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${authToken}`
      },
      body: JSON.stringify({ cartItemId, quantity: parseInt(quantity) })
    });

    if (response.ok) {
      await loadCart();
      await showCart();
    }
  } catch (error) {
    console.error('Error updating cart item:', error);
  }
}

// Remove from cart
async function removeFromCart(cartItemId) {
  if (!authToken) return;

  try {
    const response = await fetch(`${API_BASE_URL}/Cart/remove/${cartItemId}`, {
      method: 'DELETE',
      headers: {
        'Authorization': `Bearer ${authToken}`
      }
    });

    if (response.ok) {
      await loadCart();
      await showCart();
    }
  } catch (error) {
    console.error('Error removing cart item:', error);
  }
}

// Proceed to checkout
async function proceedToCheckout() {
  if (!authToken) {
    showLoginModal();
    return;
  }

  if (cartItems.length === 0) {
    alert('Your cart is empty');
    return;
  }

  const checkoutContent = document.getElementById('checkout-content');
  checkoutContent.innerHTML = `
    <div class="row">
      <div class="col-md-8">
        <h6>Order Summary</h6>
        ${cartItems.map(item => `
          <div class="d-flex justify-content-between mb-2">
            <span>${item.recipeTitle} x${item.quantity}</span>
            <span>$${(parseFloat(item.recipePrice.replace('$', '')) * item.quantity).toFixed(2)}</span>
          </div>
        `).join('')}
        <hr>
        <div class="d-flex justify-content-between fw-bold">
          <span>Total:</span>
          <span>$${cartItems.reduce((total, item) => total + (parseFloat(item.recipePrice.replace('$', '')) * item.quantity), 0).toFixed(2)}</span>
        </div>
      </div>
      <div class="col-md-4">
        <div class="d-grid gap-2">
          <button class="btn btn-success" onclick="processCheckout()">
            <i class="bi bi-credit-card me-1"></i>Complete Order
          </button>
          <button class="btn btn-outline-secondary" onclick="showCart()">
            Back to Cart
          </button>
        </div>
      </div>
    </div>
  `;

  const cartModal = bootstrap.Modal.getInstance(document.getElementById('cartModal'));
  cartModal.hide();
  
  const checkoutModal = new bootstrap.Modal(document.getElementById('checkoutModal'));
  checkoutModal.show();
}

// Process checkout
async function processCheckout() {
  if (!authToken) return;

  try {
    const response = await fetch(`${API_BASE_URL}/Order/checkout`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${authToken}`
      },
      body: JSON.stringify({ notes: '' })
    });

    if (response.ok) {
      const order = await response.json();
      
      alert(`Order placed successfully! Order #${order.orderNumber}`);
      
      const checkoutModal = bootstrap.Modal.getInstance(document.getElementById('checkoutModal'));
      checkoutModal.hide();
      
      await loadCart();
    } else {
      throw new Error('Checkout failed');
    }
  } catch (error) {
    console.error('Error processing checkout:', error);
    alert('Checkout failed. Please try again.');
  }
}

// Show order history
async function showOrderHistory() {
  if (!authToken) {
    showLoginModal();
    return;
  }

  try {
    const response = await fetch(`${API_BASE_URL}/Order`, {
      headers: {
        'Authorization': `Bearer ${authToken}`
      }
    });

    if (response.ok) {
      const orders = await response.json();
      
      const orderHistoryContent = document.getElementById('order-history-content');
      
      if (orders.length === 0) {
        orderHistoryContent.innerHTML = '<p class="text-center text-muted">No orders found</p>';
      } else {
        orderHistoryContent.innerHTML = orders.map(order => `
          <div class="card mb-3">
            <div class="card-header d-flex justify-content-between align-items-center">
              <h6 class="mb-0">Order #${order.orderNumber}</h6>
              <span class="badge bg-${order.status === 'Pending' ? 'warning' : order.status === 'Delivered' ? 'success' : 'info'}">${order.status}</span>
            </div>
            <div class="card-body">
              <div class="row">
                <div class="col-md-8">
                  <h6>Items:</h6>
                  ${order.items.map(item => `
                    <div class="d-flex justify-content-between mb-1">
                      <span>${item.recipeTitle} x${item.quantity}</span>
                      <span>$${item.totalPrice.toFixed(2)}</span>
                    </div>
                  `).join('')}
                </div>
                <div class="col-md-4 text-end">
                  <p class="mb-1"><strong>Total: $${order.totalAmount.toFixed(2)}</strong></p>
                  <small class="text-muted">Ordered: ${new Date(order.orderDate).toLocaleDateString()}</small>
                </div>
              </div>
            </div>
          </div>
        `).join('');
      }
    }
  } catch (error) {
    console.error('Error loading order history:', error);
  }

  const modal = new bootstrap.Modal(document.getElementById('orderHistoryModal'));
  modal.show();
}

// Add meal plan to cart
async function addMealPlanToCart(mealPlanId) {
  if (!authToken) {
    showLoginModal();
    return;
  }

  const mealPlan = mealPlans.find(mp => mp.id === mealPlanId);
  if (!mealPlan || !mealPlan.mealPlanRecipes || mealPlan.mealPlanRecipes.length === 0) {
    alert('No recipes found in this meal plan');
    return;
  }

  try {
    // Add each recipe from the meal plan to cart
    const addPromises = mealPlan.mealPlanRecipes.map(mpr => 
      fetch(`${API_BASE_URL}/Cart/add`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${authToken}`
        },
        body: JSON.stringify({ recipeId: mpr.recipeId, quantity: 1 })
      })
    );

    const results = await Promise.all(addPromises);
    const failedCount = results.filter(r => !r.ok).length;

    if (failedCount === 0) {
      await loadCart();
      
      // Show success feedback
      const button = document.getElementById(`add-meal-plan-${mealPlanId}`);
      const originalText = button.innerHTML;
      button.innerHTML = '<i class="bi bi-check me-1"></i>Added!';
      button.classList.remove('btn-primary');
      button.classList.add('btn-success');
      
      setTimeout(() => {
        button.innerHTML = originalText;
        button.classList.remove('btn-success');
        button.classList.add('btn-primary');
      }, 2000);
      
      alert(`Successfully added ${mealPlan.mealPlanRecipes.length} recipes from "${mealPlan.title}" to your cart!`);
    } else {
      throw new Error(`Failed to add ${failedCount} recipes`);
    }
  } catch (error) {
    console.error('Error adding meal plan to cart:', error);
    alert('Failed to add meal plan to cart. Please try again.');
  }
}

// Setup form event listeners
function setupAuthEventListeners() {
  // Login form
  document.getElementById('loginForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;
    await login(email, password);
  });

  // Register form
  document.getElementById('registerForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const userData = {
      firstName: document.getElementById('firstName').value,
      lastName: document.getElementById('lastName').value,
      email: document.getElementById('registerEmail').value,
      password: document.getElementById('registerPassword').value,
      phone: document.getElementById('phone').value,
      address: document.getElementById('address').value,
      city: document.getElementById('city').value,
      state: document.getElementById('state').value,
      zipCode: document.getElementById('zipCode').value
    };
    await register(userData);
  });
}

// Update the main initialization
document.addEventListener('DOMContentLoaded', async function() {
  try {
    // Initialize authentication
    initAuth();
    
    // Load data from API
    recipes = await fetchRecipes();
    mealPlans = await fetchMealPlans();
    categories = await fetchCategories();
    
    // Render the UI
    renderRecipes();
    renderMealPlans();
    setupEventListeners();
    setupAuthEventListeners();
  } catch (error) {
    console.error('Error initializing app:', error);
    // Fallback to empty arrays if API fails
    recipes = [];
    mealPlans = [];
    categories = [];
    renderRecipes();
    renderMealPlans();
    setupEventListeners();
    setupAuthEventListeners();
  }
});
