function ShowConfirmation() {
    var show = document.getElementById("show");
    var hide = document.getElementById("hide");
    show.style.display = "block";
    hide.style.display = "none";
}
function ShowCartItem() {
    var CartItem = document.getElementById("fab-cart");
    var CartItemCount = document.getElementById("fab-counter");
    CartItem.style.display = "flex";
    CartItemCount.style.display = "flex";
}
function alart(massege) {
    CrispyToast.success(massege);
}
function alartError(massege) {
    CrispyToast.error(massege);
}