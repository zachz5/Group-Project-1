// EzEats - Main JavaScript

// Recipe data organized by categories
const recipes = [
  // No-Cook Recipes
  {
    id: 1,
    title: "Overnight Oats",
    description: "Creamy overnight oats with peanut butter and fruit - perfect for busy mornings.",
    prepTime: "5 min prep + overnight chill",
    difficulty: "Easy",
    category: "No-Cook",
    price: "$1.00",
    image: "./client/images/OvernightOats.jpg",
    ingredients: ["½ cup rolled oats", "½ cup milk (or yogurt)", "1 tbsp peanut butter", "fruit", "chia seeds (optional)"],
    instructions: [
      "In a jar or container, mix oats and milk",
      "Add peanut butter and stir well",
      "Add fruit and chia seeds if desired",
      "Cover and refrigerate overnight"
    ]
  },
  {
    id: 2,
    title: "Tuna Salad Wraps",
    description: "Quick and protein-packed tuna wraps perfect for lunch.",
    prepTime: "5 min",
    difficulty: "Easy",
    category: "No-Cook",
    price: "$1.50",
    image: "./client/images/TunaWrap.jpg",
    ingredients: ["1 can tuna", "1 tbsp mayo or Greek yogurt", "tortilla", "lettuce"],
    instructions: [
      "Drain tuna and mix with mayo or Greek yogurt",
      "Spoon tuna mixture into tortilla",
      "Add lettuce leaves",
      "Roll up tightly and serve"
    ]
  },
  {
    id: 3,
    title: "Hummus & Veggie Pita",
    description: "Fresh and healthy pita stuffed with hummus and crisp vegetables.",
    prepTime: "5 min",
    difficulty: "Easy",
    category: "No-Cook",
    price: "$1.25",
    image: "./client/images/PitaWrap.jpg",
    ingredients: ["pita bread", "3 tbsp hummus", "cucumber slices", "carrot sticks", "spinach"],
    instructions: [
      "Cut pita bread in half",
      "Spread hummus inside both halves",
      "Add cucumber slices and carrot sticks",
      "Stuff with spinach leaves and serve"
    ]
  },
  {
    id: 4,
    title: "Peanut Butter Banana Sandwich",
    description: "Classic comfort food with a sweet honey drizzle.",
    prepTime: "3 min",
    difficulty: "Easy",
    category: "No-Cook",
    price: "$0.75",
    image: "./client/images/PenutButterandBanana.jpg",
    ingredients: ["2 slices bread", "2 tbsp peanut butter", "1 banana", "honey (optional)"],
    instructions: [
      "Spread peanut butter on both slices of bread",
      "Slice banana and arrange on one slice",
      "Drizzle with honey if desired",
      "Close sandwich and enjoy"
    ]
  },
  {
    id: 5,
    title: "Greek Yogurt Parfait",
    description: "Layered parfait with Greek yogurt, granola, and fresh berries.",
    prepTime: "3 min",
    difficulty: "Easy",
    category: "No-Cook",
    price: "$1.25",
    image: "./client/images/YogurtParfait.jpg",
    ingredients: ["1 cup Greek yogurt", "¼ cup granola", "½ cup berries", "honey (optional)"],
    instructions: [
      "Layer half the yogurt in a cup or bowl",
      "Add half the granola and berries",
      "Repeat layers with remaining ingredients",
      "Drizzle with honey and serve"
    ]
  },
  // Microwave Recipes
  {
    id: 6,
    title: "Microwave Mac & Cheese",
    description: "Creamy mac and cheese made entirely in the microwave.",
    prepTime: "8 min",
    difficulty: "Easy",
    category: "Microwave",
    price: "$1.25",
    image: "./client/images/MicrowaveMac.jpg",
    ingredients: ["½ cup pasta", "½ cup water", "¼ cup shredded cheese", "2 tbsp milk"],
    instructions: [
      "In a microwave-safe mug, add pasta and water",
      "Microwave 4-5 minutes until pasta is soft, stirring halfway",
      "Stir in cheese and milk until creamy",
      "Microwave 30 seconds more if needed"
    ]
  },
  {
    id: 7,
    title: "Microwave Baked Potato",
    description: "Perfectly cooked baked potato with your favorite toppings.",
    prepTime: "6 min",
    difficulty: "Easy",
    category: "Microwave",
    price: "$0.80",
    image: "./client/images/BakedPotato.jpg",
    ingredients: ["1 potato", "sour cream", "cheese", "green onion"],
    instructions: [
      "Wash potato and poke holes with a fork",
      "Microwave 5-6 minutes, flipping halfway",
      "Let cool slightly, then cut open",
      "Top with sour cream, cheese, and green onion"
    ]
  },
  {
    id: 8,
    title: "Microwave Egg Scramble",
    description: "Fluffy scrambled eggs with cheese and veggies - microwave style.",
    prepTime: "4 min",
    difficulty: "Easy",
    category: "Microwave",
    price: "$1.00",
    image: "./client/images/ScrambledEggs.jpg",
    ingredients: ["2 eggs", "2 tbsp milk", "shredded cheese", "diced veggies"],
    instructions: [
      "Whisk eggs and milk in a microwave-safe mug",
      "Microwave 1 minute, then stir",
      "Cook another 45 seconds to 1 minute",
      "Stir in cheese and veggies while hot"
    ]
  },
  {
    id: 9,
    title: "Microwave Quesadilla",
    description: "Cheesy quesadilla made quickly in the microwave.",
    prepTime: "4 min",
    difficulty: "Easy",
    category: "Microwave",
    price: "$1.25",
    image: "./client/images/Quesadilla.jpg",
    ingredients: ["1 tortilla", "½ cup shredded cheese", "salsa", "beans/chicken (optional)"],
    instructions: [
      "Place tortilla on a microwave-safe plate",
      "Sprinkle cheese evenly over half",
      "Add beans or chicken if desired",
      "Fold in half and microwave 1-2 minutes until cheese melts"
    ]
  },
  {
    id: 10,
    title: "Microwave Mug Pizza",
    description: "Personal pizza made entirely in a mug - perfect for one!",
    prepTime: "7 min",
    difficulty: "Medium",
    category: "Microwave",
    price: "$1.50",
    image: "./client/images/MugPizza.jpg",
    ingredients: ["4 tbsp flour", "3 tbsp water", "½ tsp baking powder", "2 tbsp marinara", "2 tbsp mozzarella", "pepperoni"],
    instructions: [
      "In a mug, mix flour, water, and baking powder into dough",
      "Spread marinara sauce on top of dough",
      "Add mozzarella cheese and pepperoni",
      "Microwave 1-2 minutes until cooked through"
    ]
  },
  // Stovetop Recipes
  {
    id: 11,
    title: "One-Pot Ramen Upgrade",
    description: "Transform basic ramen into a gourmet meal with egg and veggies.",
    prepTime: "7 min",
    difficulty: "Easy",
    category: "Stovetop",
    price: "$1.00",
    image: "./client/images/OnePotRamen.jpg",
    ingredients: ["1 ramen packet", "1 egg", "½ cup frozen veggies", "soy sauce"],
    instructions: [
      "Boil noodles in 2 cups water according to package directions",
      "Add frozen veggies to the pot",
      "Crack egg into the pot and stir until cooked",
      "Add seasoning packet and soy sauce to taste"
    ]
  },
  {
    id: 12,
    title: "Grilled Cheese & Tomato Soup",
    description: "Classic comfort food combo - crispy grilled cheese with warm tomato soup.",
    prepTime: "10 min",
    difficulty: "Easy",
    category: "Stovetop",
    price: "$1.75",
    image: "./client/images/GrilledCheese.jpg",
    ingredients: ["2 slices bread", "2 slices cheese", "butter", "1 can tomato soup"],
    instructions: [
      "Heat tomato soup in a small pot",
      "Butter bread and add cheese between slices",
      "Grill in a pan until golden brown on both sides",
      "Serve grilled cheese with hot tomato soup"
    ]
  },
  {
    id: 13,
    title: "Fried Rice with Egg",
    description: "Quick and satisfying fried rice using leftover rice and frozen veggies.",
    prepTime: "12 min",
    difficulty: "Easy",
    category: "Stovetop",
    price: "$1.25",
    image: "./client/images/FriedRiceEgg.jpg",
    ingredients: ["1 cup leftover rice", "1 egg", "½ cup frozen peas/carrots", "1 tbsp soy sauce"],
    instructions: [
      "Heat oil in a pan over medium heat",
      "Scramble the egg and remove from pan",
      "Add rice and frozen veggies, stir-fry until heated",
      "Return egg to pan, add soy sauce, and stir everything together"
    ]
  },
  {
    id: 14,
    title: "Simple Pasta with Garlic & Oil",
    description: "Elegant pasta dish with garlic, olive oil, and red pepper flakes.",
    prepTime: "12 min",
    difficulty: "Easy",
    category: "Stovetop",
    price: "$1.25",
    image: "./client/images/Pasta.jpg",
    ingredients: ["1 cup pasta", "2 tbsp olive oil", "1 clove garlic", "red pepper flakes"],
    instructions: [
      "Cook pasta according to package directions",
      "In a pan, heat olive oil over medium heat",
      "Sauté minced garlic until golden",
      "Toss cooked pasta with garlic oil and red pepper flakes"
    ]
  },
  {
    id: 15,
    title: "Veggie Stir-Fry with Rice",
    description: "Colorful vegetable stir-fry served over rice - healthy and delicious.",
    prepTime: "15 min",
    difficulty: "Easy",
    category: "Stovetop",
    price: "$1.50",
    image: "./client/images/StirFry.jpg",
    ingredients: ["1 cup frozen stir-fry veggies", "1 tbsp soy sauce", "1 cup rice"],
    instructions: [
      "Cook rice (microwave or stovetop)",
      "In a pan, stir-fry frozen veggies until heated through",
      "Add soy sauce and continue cooking 2-3 minutes",
      "Serve stir-fry over cooked rice"
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

// Current filter state
let currentFilter = 'all';
let showAllRecipes = false;

// Initialize the app
document.addEventListener('DOMContentLoaded', function() {
  renderRecipes();
  renderMealPlans();
  setupEventListeners();
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
    : recipes.filter(recipe => recipe.category === currentFilter);
  
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
            <span class="badge bg-${getCategoryColor(recipe.category)}">
              <i class="bi ${getCategoryIcon(recipe.category)} me-1"></i>${recipe.category}
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
          <button class="btn btn-outline-primary btn-sm w-100" onclick="showRecipeDetails(${recipe.id})">
            View Recipe
          </button>
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
