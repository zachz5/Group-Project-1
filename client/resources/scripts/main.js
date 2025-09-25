// EzEats - Main JavaScript

// Sample recipe data
const recipes = [
  {
    id: 1,
    title: "Microwave Mac & Cheese",
    description: "Creamy mac and cheese made in just 5 minutes using only a microwave.",
    prepTime: "5 min",
    difficulty: "Easy",
    image: "https://images.unsplash.com/photo-1543339494-b4cd4f7ba686?w=400&h=300&fit=crop",
    ingredients: ["1 cup pasta", "1 cup water", "1/2 cup cheese", "2 tbsp milk", "Salt & pepper"],
    instructions: [
      "Combine pasta and water in microwave-safe bowl",
      "Microwave on high for 3-4 minutes",
      "Stir in cheese, milk, salt, and pepper",
      "Microwave 1 more minute and stir"
    ]
  },
  {
    id: 2,
    title: "Dorm Room Quesadilla",
    description: "Crispy quesadilla made with just a microwave and basic ingredients.",
    prepTime: "3 min",
    difficulty: "Easy",
    image: "https://images.unsplash.com/photo-1565299624946-b28f40a0ca4b?w=400&h=300&fit=crop",
    ingredients: ["2 tortillas", "1/2 cup cheese", "1/4 cup beans", "Salsa", "Sour cream"],
    instructions: [
      "Place cheese and beans on one tortilla",
      "Top with second tortilla",
      "Microwave for 1-2 minutes until cheese melts",
      "Cut into wedges and serve with salsa"
    ]
  },
  {
    id: 3,
    title: "Microwave Scrambled Eggs",
    description: "Fluffy scrambled eggs made in the microwave - perfect for breakfast.",
    prepTime: "4 min",
    difficulty: "Easy",
    image: "https://images.unsplash.com/photo-1525351484163-7529414344d8?w=400&h=300&fit=crop",
    ingredients: ["2 eggs", "2 tbsp milk", "Salt & pepper", "Butter", "Cheese (optional)"],
    instructions: [
      "Beat eggs with milk, salt, and pepper",
      "Add butter to microwave-safe bowl",
      "Pour in egg mixture and microwave 30 seconds",
      "Stir and repeat until eggs are set"
    ]
  },
  {
    id: 4,
    title: "Instant Ramen Upgrade",
    description: "Transform basic ramen into a gourmet meal with simple additions.",
    prepTime: "7 min",
    difficulty: "Easy",
    image: "https://images.unsplash.com/photo-1569718212165-3a8278d5f624?w=400&h=300&fit=crop",
    ingredients: ["1 pack ramen", "1 egg", "Green onions", "Soy sauce", "Sesame oil"],
    instructions: [
      "Cook ramen according to package directions",
      "Add beaten egg while stirring",
      "Top with green onions and drizzle with soy sauce",
      "Finish with a few drops of sesame oil"
    ]
  }
];

// Sample meal plan data
const mealPlans = [
  {
    id: 1,
    title: "Budget Week",
    description: "Affordable meals under $20 for the week",
    price: "$15-20",
    recipes: ["Microwave Mac & Cheese", "Dorm Room Quesadilla", "Instant Ramen Upgrade"],
    duration: "7 days"
  },
  {
    id: 2,
    title: "Protein Power",
    description: "High-protein meals for active students",
    price: "$25-30",
    recipes: ["Microwave Scrambled Eggs", "Dorm Room Quesadilla", "Instant Ramen Upgrade"],
    duration: "7 days"
  },
  {
    id: 3,
    title: "Quick & Easy",
    description: "All meals ready in under 10 minutes",
    price: "$20-25",
    recipes: ["Microwave Mac & Cheese", "Microwave Scrambled Eggs", "Instant Ramen Upgrade"],
    duration: "5 days"
  }
];

// DOM elements
const recipesContainer = document.getElementById('recipes-container');
const mealPlansContainer = document.getElementById('meal-plans-container');

// Initialize the app
document.addEventListener('DOMContentLoaded', function() {
  renderRecipes();
  renderMealPlans();
  setupEventListeners();
});

// Render recipes
function renderRecipes() {
  if (!recipesContainer) return;
  
  recipesContainer.innerHTML = recipes.map(recipe => `
    <div class="col-lg-3 col-md-6 mb-4">
      <div class="recipe-card h-100">
        <div class="recipe-image" style="background-image: url('${recipe.image}')"></div>
        <div class="p-3">
          <h5 class="fw-bold">${recipe.title}</h5>
          <p class="text-muted small">${recipe.description}</p>
          <div class="d-flex justify-content-between align-items-center mb-2">
            <small class="text-primary"><i class="bi bi-clock me-1"></i>${recipe.prepTime}</small>
            <small class="text-success"><i class="bi bi-check-circle me-1"></i>${recipe.difficulty}</small>
          </div>
          <button class="btn btn-outline-primary btn-sm w-100" onclick="showRecipeDetails(${recipe.id})">
            View Recipe
          </button>
        </div>
      </div>
    </div>
  `).join('');
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
              ${plan.recipes.map(recipe => `<li><i class="bi bi-check text-success me-2"></i>${recipe}</li>`).join('')}
            </ul>
          </div>
          <button class="btn btn-primary w-100" onclick="selectMealPlan(${plan.id})">
            Select Plan
          </button>
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
  ingredientsList.innerHTML = recipe.ingredients.map(ingredient => 
    `<li class="list-group-item">${ingredient}</li>`
  ).join('');
  
  // Update instructions
  const instructionsList = document.getElementById('recipeInstructions');
  instructionsList.innerHTML = recipe.instructions.map((instruction, index) => 
    `<li class="list-group-item"><strong>Step ${index + 1}:</strong> ${instruction}</li>`
  ).join('');
  
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
  
  alert(`You selected the "${plan.title}" meal plan! This would typically redirect to a checkout or planning page.`);
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
