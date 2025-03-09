// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    let searchInput = document.getElementById("searchInput");
    let searchResults = document.getElementById("searchResults");

    searchInput.addEventListener("keyup", function () {
        let keyword = searchInput.value.trim();

        if (keyword.length === 0) {
            searchResults.innerHTML = "";
            return;
        }

        fetch(`/SearchProduct/Search?keyword=${encodeURIComponent(keyword)}`)
            .then(response => response.json())
            .then(data => {
                searchResults.innerHTML = "";

                if (!Array.isArray(data) || data.length === 0) {
                    searchResults.innerHTML = `<div class="alert alert-warning p-2">Không tìm thấy kết quả</div>`;
                    return;
                }

                data.forEach(item => {
                    let resultItem = document.createElement("div");
                    resultItem.classList.add("search-item");
                    resultItem.innerHTML = `
                            <img src="${item.image || 'default-image.jpg'}" alt="${item.tenThuoc}" class="product-img">
                            <div class="product-info">
                                <strong>${item.tenThuoc}</strong>
                                <p>Giá: <span class="text-danger">${item.giaBan} VNĐ</span></p>
                                <p>Số lượng: <span class="text-success">${item.soLuong}</span></p>
                            </div>
                        `;
                    resultItem.onclick = function () {
                        searchInput.value = item.tenThuoc;
                        searchResults.innerHTML = "";
                    };
                    searchResults.appendChild(resultItem);
                });
            })
            .catch(error => {
                console.error("Lỗi:", error);
                searchResults.innerHTML = `<div class="alert alert-danger p-2">Lỗi server. Vui lòng thử lại.</div>`;
            });
    });

    document.addEventListener("click", function (event) {
        if (!searchInput.contains(event.target) && !searchResults.contains(event.target)) {
            searchResults.innerHTML = "";
        }
    });
});