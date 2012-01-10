$(document).ready(function () {
    var dialog = $('#product-dialog');
    dialog.dialog({
        bgiframe: true,
        autoOpen: false,
        show: 'scale',
        hide: 'scale',
        height: 'auto',
        width: '400',
        modal: true,
        buttons: {
            'Save': function () {
                $('form', dialog).correlatedSubmit();
            },
            'Cancel': function () {
                dialog.dialog('close');
            }
        }
    });

    $('#new-product').click(function() {
        dialog.dialog('open');
    });

    var form = $('form', dialog);
    var preview = form.data('validation-preview');
    $(':input', form).change(function () {
        $.ajax({
            url: preview,
            type: 'POST',
            data: form.formSerialize(),
            beforeSend: function (xhr) {
                this.correlationId = form.attr('id');
                $.continuations.setupRequest.call(this, xhr);
            }
        });
    });
});