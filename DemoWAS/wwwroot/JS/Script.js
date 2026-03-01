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
window.registerScrollHandler = function (dotNetHelper, MethodName) {
    window.onscroll = function () {
        const scrollPosition = window.scrollY + window.innerHeight;
        const triggerPoint = document.body.offsetHeight * 0.7;
        if (scrollPosition >= triggerPoint) {
            dotNetHelper.invokeMethodAsync(MethodName);
        }
    };
};
window.removeScrollListener = function () {
    window.onscroll = null;
};
function ShowItem(show) {
    var show = document.getElementById(show);
    show.style.display = "flex";
}
function alartNotification(massege) {
    iziToast.show({
        title: 'تنبيه',
        message: massege,
        position: 'topCenter',
        color: 'orange',
        timeout: 15000,
        progressBarColor: 'white',
        onOpening: function () {
            document.getElementById('notif').play();
        }
    });
}
function alart(massege) {
    iziToast.show({
        title: 'نجاح',
        message: massege,
        position: 'topCenter',
        color: 'green',
        timeout: 15000,
        progressBarColor: 'white',
    });
}
function alartError(massege) {
    iziToast.show({
        title: 'خطأ',
        message: massege,
        position: 'topCenter',
        color: 'red',
        timeout: 6000,
        progressBarColor: 'white',
    });
}