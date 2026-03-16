import './assets/app.css';
import { getBicycles, createBicycle, updateBicycle, deleteBicycle } from './js/bicycle.js';
import { formatPrice } from './js/helper.js';

let bicycles = [];

const bicyclesContainer = document.querySelector('#bicycles');
const bicycleCardTemplate = document.querySelector('#bicycle-card');
const bicycleDialog = document.querySelector('#bicycle-dialog');
const bicycleFormDialog = document.querySelector('#bicycle-form-dialog');
const confirmDialog = document.querySelector('#confirm-dialog');
const bicycleForm = document.querySelector('#bicycle-form');
const addButton = document.querySelector('#add-button');

function createCard(bicycle) {
  const clone = bicycleCardTemplate.content.cloneNode(true);
  const card = clone.querySelector('.card');
  
  card.dataset.id = bicycle.id;
  
  const h3 = clone.querySelector('h3');
  h3.textContent = `${bicycle.manufacturer} ${bicycle.name}`;
  
  const img = clone.querySelector('img');
  img.src = `images/${bicycle.id}.jpg`;
  img.alt = `${bicycle.manufacturer} ${bicycle.name}`;
  
  const priceP = clone.querySelector('p');
  priceP.textContent = `Ár: ${formatPrice(bicycle.price)}`;
  
  const detailsBtn = clone.querySelector('.details');
  detailsBtn.addEventListener('click', () => {
    updateBicycleDialog(bicycle);
    bicycleDialog.showModal();
  });
  
  const editBtn = clone.querySelector('.edit');
  editBtn.addEventListener('click', () => {
    showUpdateDialog(bicycle);
  });
  
  const deleteBtn = clone.querySelector('.delete');
  deleteBtn.addEventListener('click', () => {
    confirmDialog.dataset.id = bicycle.id;
    showConfirmDialog();
  });
  
  return clone;
}

function generateCards(bicyclesArray) {
  bicyclesContainer.innerHTML = '';
  bicyclesArray.forEach(bicycle => {
    bicyclesContainer.appendChild(createCard(bicycle));
  });
}

function updateBicycleDialog(bicycle) {
  const h1 = bicycleDialog.querySelector('h1');
  h1.textContent = `${bicycle.manufacturer} ${bicycle.name}`;
  
  const img = bicycleDialog.querySelector('img');
  img.src = `images/${bicycle.id}.jpg`;
  
  const detailsList = bicycleDialog.querySelector('ul.details');
  detailsList.innerHTML = `
    <li><span class="font-semibold text-gray-800">Kerék méret:</span> ${bicycle.wheel_size} col</li>
    <li><span class="font-semibold text-gray-800">Váltó:</span> ${bicycle.speed} sebességes</li>
    <li><span class="font-semibold text-gray-800">Nem:</span> ${bicycle.sex}</li>
    <li><span class="font-semibold text-gray-800">Típus:</span> ${bicycle.type}</li>
    <li><span class="font-semibold text-gray-800">Szín:</span> ${bicycle.color}</li>
    <li><span class="font-semibold text-gray-800">Ár:</span> ${formatPrice(bicycle.price)}</li>
  `;
}

function showCreateDialog() {
  bicycleFormDialog.dataset.id = '';
  bicycleFormDialog.querySelector('h1').textContent = 'Új kerékpár';
  bicycleForm.reset();
  
  const radios = bicycleForm.querySelectorAll('input[type="radio"]');
  radios.forEach(radio => radio.checked = false);
  
  bicycleFormDialog.showModal();
}

function showUpdateDialog(bicycle) {
  bicycleFormDialog.dataset.id = bicycle.id;
  bicycleFormDialog.querySelector('h1').textContent = bicycle.name;
  
  bicycleForm.manufacturer.value = bicycle.manufacturer;
  bicycleForm.name.value = bicycle.name;
  bicycleForm.wheel_size.value = bicycle.wheel_size;
  bicycleForm.speed.value = bicycle.speed;
  bicycleForm.type.value = bicycle.type;
  bicycleForm.price.value = bicycle.price;
  bicycleForm.color.value = bicycle.color;
  
  const sexRadio = bicycleForm.querySelector(`input[name="sex"][value="${bicycle.sex}"]`);
  if (sexRadio) sexRadio.checked = true;
  
  bicycleFormDialog.showModal();
}

function showConfirmDialog() {
  confirmDialog.showModal();
}

addButton.addEventListener('click', showCreateDialog);

bicycleForm.addEventListener('submit', async (e) => {
  e.preventDefault();
  
  const formData = new FormData(bicycleForm);
  const bicycleData = Object.fromEntries(formData.entries());
  
  bicycleData.wheel_size = parseFloat(bicycleData.wheel_size);
  bicycleData.speed = parseInt(bicycleData.speed);
  bicycleData.price = parseInt(bicycleData.price);
  
  const id = bicycleFormDialog.dataset.id;
  
  try {
    if (!id) {
      const newBicycle = await createBicycle(bicycleData);
      bicycles.push(newBicycle);
    } else {
      bicycleData.id = id;
      const updatedBicycle = await updateBicycle(bicycleData);
      const index = bicycles.findIndex(b => b.id == id);
      if (index !== -1) {
        bicycles[index] = updatedBicycle;
      }
    }
    
    generateCards(bicycles);
    bicycleFormDialog.close();
  } catch (error) {
    alert(error.message);
  }
});

document.querySelector('#close-form-button').addEventListener('click', () => {
  bicycleFormDialog.close();
});

confirmDialog.querySelector('.delete').addEventListener('click', async () => {
  const id = confirmDialog.dataset.id;
  try {
    await deleteBicycle(id);
    bicycles = bicycles.filter(b => b.id != id);
    generateCards(bicycles);
    confirmDialog.close();
  } catch (error) {
    alert(error.message);
  }
});

confirmDialog.querySelector('.cancel').addEventListener('click', () => {
  confirmDialog.close();
});

async function init() {
  try {
    bicycles = await getBicycles();
    generateCards(bicycles);
  } catch (error) {
    console.error(error.message);
  }
}

init();
