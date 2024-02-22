AddItemToCart = function (e) {

    // lấy giỏ ra để bắt đầu đi nhặt hàng
    var store = GetCookie('MyShoppingCart');

    // đi nhặt hàng
    var item = {
        ProductID: $(e).data('productid'),
        ProductName: $(e).data('productname'),
        Quality: 1,
        Price: $(e).data('price'),
        Image: $(e).data('image')
    };

    // bỏ vào trong giỏ
    AddAndUpdateShoppingCart(store, item);

    alert("Thêm vào giỏ thành công!");

}

RemoveItemFromCart = function (e) {

    // xử lý gì ??

    var result = confirm("Bạn có chắc chắn muốn xóa bản ghi này ??");
    if (result) {

        // lấy giỏ ra để bắt đầu đi nhặt hàng
        var store = GetCookie('MyShoppingCart');

        var item = {
            ProductID: $(e).data('productid'),
            ProductName: $(e).data('productname'),
            Quality: 1,
            Price: $(e).data('price'),
            Image: $(e).data('image')
        };

        // thực hiện xóa
        RemoveItemFromShoppingCart(store, item);

        window.location.href = "/ShoppingCart/Index";
    }
}

UpdateCartItem = function (e) {
    // xử lý gì ở đây
    // lấy giá trị của ô input
    var quantity = $("#txtQuantity").val();

    // lấy giỏ ra để bắt đầu đi nhặt hàng
    var store = GetCookie('MyShoppingCart');

    var item = {
        ProductID: $(e).data('productid'),
        ProductName: $(e).data('productname'),
        Quality: 1,
        Price: $(e).data('price'),
        Image: $(e).data('image')
    };
    // kiểm tra xem trong giỏ có sản phẩm đó ko 
    var index = store.findIndex(function (d) {
        return d.ProductID == item.ProductID;
    });

    // nếu không có thì dừng luôn
    if (store.length == 0 || index == -1) {
        return;
    }

    // ngược lại thì set lại số lượng của sản phẩm đó trong giỏ

    store[index].Quality = quantity;
    SetCookie('MyShoppingCart', store);

    window.location.href = "/ShoppingCart/Index";

}

AddAndUpdateShoppingCart = function (store, item, quantity) {

    var index = store.findIndex(function (d) {
        return d.ProductID == item.ProductID;
    });

    if (store.length == 0 || index == -1) {
        store.push(item);
        SetCookie('MyShoppingCart', store);
        return store;
    }
    if (quantity != null && quantity != "undefined") {
        store[index].Quality = quantity;
    } else {
        store[index].Quality = parseInt(store[index].Quality) + 1;
    }
    SetCookie('MyShoppingCart', store);
    return store;
}

RemoveItemFromShoppingCart = function (store, item) {

    if (store.length > 0) {
        var index = store.findIndex(function (d) {
            return d.ProductID == item.ProductID;
        });

        store.splice(index, 1);
        SetCookie('MyShoppingCart', store);
        return store;
    }
    //store[index].Quality = parseInt(store[index].Quality) - 1;
    //SetCookie('ShoppingCart', store);
    //return store;
}