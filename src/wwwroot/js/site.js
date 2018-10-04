// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function() {
    $(".comment-btn").click(function(){
        addComment();
    });
});



function addComment() {
    $.ajax ({
        type: 'POST',
        url: '/api/v1/CommentApi/AddComment/' + $(".comment-btn").data().movieid,
        data: {
            comment: $("#userComment").val(),
        },
        success: function(response){
            var ul = $("#commentList");
            var li = $("<li></li>");
            li.append(document.createTextNode(response.comment));
            ul.append(li);
        },
    });
}
