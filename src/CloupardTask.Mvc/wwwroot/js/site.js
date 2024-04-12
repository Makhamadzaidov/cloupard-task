const openPopupButton = document.getElementById('open-popup');
const popup = document.getElementById('popup');
const closePopupButton = document.querySelector('.close');

openPopupButton.addEventListener('click', function () {
    popup.style.display = 'block';
});

closePopupButton.addEventListener('click', function () {
    popup.style.display = 'none';
});

document.getElementById('add-product-form').addEventListener('submit', () => {
    alert("Product added successfully")
});

// Get the modal
const modal = document.getElementById('edit-product-modal');

// Get the <span> element that closes the modal
const closeBtn = modal.querySelector('.close');

// When the user clicks the button, open the modal
document.addEventListener('DOMContentLoaded', function () {
    const editButtons = document.querySelectorAll('.edit');
    editButtons.forEach(function (editButton) {
        editButton.addEventListener('click', function () {
            modal.style.display = 'block';

            // Populate form fields with product data
            const currentRow = this.closest('tr');
            const productId = currentRow.querySelector('input[name="Id"]').value;
            const productName = currentRow.querySelector('td:nth-child(2)').textContent;
            const productDescription = currentRow.querySelector('td:nth-child(3)').textContent;

            document.getElementById('edit-product-id').value = productId;
            document.getElementById('edit-product-name').value = productName;
            document.getElementById('edit-product-description').value = productDescription;
        });
    });
});

// When the user clicks on <span> (x), close the modal
closeBtn.addEventListener('click', function () {
    modal.style.display = 'none';
});

// When the user clicks anywhere outside of the modal, close it
window.addEventListener('click', function (event) {
    if (event.target === modal) {
        modal.style.display = 'none';
    }
});

