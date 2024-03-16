redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51OMpjbIawYUUJS9trSKpoiAP6QaEwVDci1KWgEdJ9Gyd8LeGoCVLvHd3fyHv8mMXEo7Ov1pmPLhSdrE3b2awIPg400aIszkUfw");
    stripe.redirectToCheckout({ sessionId: sessionId });
}