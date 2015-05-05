/*
<form id="registration">
    <div>
        <label for="movie">Movie:</label>
        <input id="movie">
    </div>
    <div>Favorites:<span id="favorites"></span>

    </div>
    <div>
        <input type="submit" value="Submit">
    </div>
</form>
*/

function getFavorites() {
    favorites = []
    $(".movieTitle").each(function (index) {
        favorites.push($(this).text())
    })
    return favorites
}

$("#movie").autocomplete({
    source: function (request, response) {
        url = "https://api.themoviedb.org/3/"
        funct = 'search/movie?query='
        api_key = "36c4c63002ad8a101ee74a05b8b39554"
        $.ajax({
            url: url + funct + encodeURI($("#movie").val()) + "&api_key=" + api_key,
            dataType: "jsonp",
            success: function (data) {
                titles = []
                $.each(data["results"], function (index) {
                    if ($.inArray(this["title"], titles) < 0) {
                        if ($.inArray(this["title"], getFavorites()) < 0) {
                            titles.push(this["title"])
                        }
                    }
                })
                response(titles)
            },
            error: function (e) {
                console.log(e.message)
            }
        });
    },
    minLength: 3,
    select: function (event, ui) {
        movie = $('<div/>')
        title = $('<span/>').addClass("movieTitle")
        remove = $("<span class='glyphicon glyphicon-remove' aria-hidden=true aria-label='remove' onclick='$(this).parent().remove();return false'></span>")

        title.prepend(ui["item"]["value"])
        movie.append(title)
        movie.append(remove)
        $("#favorites").append(movie)

        $("#movie").val("")
        event.preventDefault()
    },
});