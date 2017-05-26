var cart = {
    init: function () {
        cart.regEvents();
    },

    regEvents: function () {
        $ ('#btnContinue').off('click').on('click', function () {
            window.location.href = "/Home/Tour";
        });
        $('#btnCheckout').off('click').on('click', function () {
            window.location.href = "/Home/Checkout";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listTour = $('.quantity');
            var cartList = [];
            $.each(listTour, function (i, item) {
                cartList.push({
                    Quatity: $(item).val(),
                    Tour: {
                        Id: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Home/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true)
                    {
                        window.location.href = '/Home/Tour';
                    }
                }
            })
        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Home/Delete',
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/Home/Tour';
                    }
                }
            })
        });

        $('#btnDelete').off('click').on('click', function () {

            $.ajax({
                url: '/Home/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/Home/Tour';
                    }
                }
            })
        });
    }
}
cart.init();