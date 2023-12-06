$(document).ready(function () {
    $('a.page-link').click(function (e) {
        e.preventDefault(); 

        var url = $(this).attr('href'); 

        $.get(url, function (data) {
            $('#partial-content').html(data);
        });
    });
});
