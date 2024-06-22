function toggleHover(element) {
    element.classList.toggle("active");
}


// Đóng mở Image of Details Product
document.addEventListener("DOMContentLoaded", function () {
    var tabs = document.querySelectorAll(".image-more");
    tabs.forEach(function (tab) {
        tab.addEventListener("click", function (event) {
            event.preventDefault();
            var targetImage = event.target.getAttribute('src');
            displayImage(targetImage);
        });
    });

    document.getElementById('close-btn').addEventListener('click', function () {
        document.getElementById('display-image-overlay').style.display = 'none';
    });
});

function displayImage(imageSrc) {
    var displayDiv = document.getElementById('display-image-overlay');
    var displayedImage = document.getElementById('displayed-image');
    displayedImage.setAttribute('src', imageSrc);
    displayDiv.style.display = 'flex'; // Hiển thị phần hiển thị ảnh
}





