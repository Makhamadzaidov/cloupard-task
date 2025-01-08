const productCards = document.querySelectorAll('.products__cards__card');

productCards.forEach((card) => {
    card.addEventListener('click', () => {
        const productId = card.dataset.id; // Получаем ID продукта
        const productName = card.dataset.product; // Получаем название продукта

        // Формируем URL с параметрами
        const url = `/Product/ProductDetails?id=${productId}&name=${encodeURIComponent(productName)}`;

        // Переход на страницу деталей
        window.location.href = url;
    });
});
