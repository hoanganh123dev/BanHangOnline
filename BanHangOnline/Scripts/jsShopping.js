$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            quantity = parseInt(tQuantity);
        }
        //alert(id + " " + quantity);
        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);
                }
                    
            }
        });
    });
    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm('bạn có chắc muốn xóa hết sản phẩm  trong giỏ hàng không?');
        if (conf == true) {
            DeleteAll();
        }

    });

    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#Quantity_' + id).val();
        Update(id, quantity);

    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng không?');
        if (conf == true) {
            $.ajax({
                url: '/shoppingcart/Delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $('#checkout_items').html(rs.Count);
                        $('#trow_' + id).remove();
                        Loadcart();
                    }

                }
            });
        }
        
    });  
});

function ShowCount() {
    $.ajax({
        url: '/shoppingcart/ShowCount',
        type: 'GET',
        success: function (rs) {
                $('#checkout_items').html(rs.Count);
        }
    });
}
function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {
                Loadcart();
            }
        }
    });
}
function Update(id,quantity) {
    $.ajax({
        url: '/shoppingcart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                Loadcart();
            }
        }
    });
}
function Loadcart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
}