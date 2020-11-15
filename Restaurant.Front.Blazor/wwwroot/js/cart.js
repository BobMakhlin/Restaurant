
window.cart = {

    totalCount: {
        _targetNodeSelector: '.cart-link__count',

        increase: function () {
            const targetNode = document.querySelector(this._targetNodeSelector);

            const currentCount = +(targetNode.innerText);
            targetNode.innerText = currentCount + 1;
        },

        decrease: function () {
            const targetNode = document.querySelector(this._targetNodeSelector);

            const currentCount = +(targetNode.innerText);
            targetNode.innerText = currentCount - 1;
        },

        toZero: function () {
            this.set(0);
        },

        set: function (value) {
            const targetNode = document.querySelector(this._targetNodeSelector);
            targetNode.innerText = value;
        }
    }

}
