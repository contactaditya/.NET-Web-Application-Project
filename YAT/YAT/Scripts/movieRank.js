var fill = d3.scale.category20b();

var w = 960,
    h = 600;

var layout = d3.layout.cloud()
    .timeInterval(10)
    .size([w, h])
    .fontSize(function (d) { return fontSize(+d.value); })
    .text(function (d) { return d.name; })
    .on("end", draw);

var svg = d3.select("#movieRank").append("svg").attr("width", w).attr("height", h);
vis = svg.append("g").attr("transform", "translate(" + [w >> 1, h >> 1] + ")");

function draw(data, bounds) {
    scale = bounds ? Math.min(
        w / Math.abs(bounds[1].x - w / 2),
        w / Math.abs(bounds[0].x - w / 2),
        h / Math.abs(bounds[1].y - h / 2),
        h / Math.abs(bounds[0].y - h / 2)) / 2 : 1;
    words = data;
    var text = vis.selectAll("text")
        .data(words, function (d) { return d.text.toLowerCase(); });
    text.transition()
        .duration(1000)
        .attr("transform", function (d) { return "translate(" + [d.x, d.y] + ")rotate(" + d.rotate + ")"; })
        .style("font-size", function (d) { return d.size + "px"; });
    text.enter().append("text")
        .attr("text-anchor", "middle")
        .attr("transform", function (d) { return "translate(" + [d.x, d.y] + ")rotate(" + d.rotate + ")"; })
        .style("font-size", "1px")
      .transition()
        .duration(1000)
        .style("font-size", function (d) { return d.size + "px"; });
    text.style("font-family", function (d) { return d.font; })
        .style("fill", function (d) { return fill(d.text.toLowerCase()); })
        .style("cursor", "pointer")
        .text(function (d) { return d.text; })
        .on("click", function (d, i){
            window.open("http://www.imdb.com/find?ref_=nv_sr_fn&q="+encodeURIComponent(d.name), "_blank");
        });
    vis.transition()
        .delay(1000)
        .duration(750)
        .attr("transform", "translate(" + [w >> 1, h >> 1] + ")scale(" + scale + ")");
}

(function () {
    var r = 40.5,
        px = 35,
        py = 20;

    var radians = Math.PI / 180,
        scale = d3.scale.linear(),
        arc = d3.svg.arc().innerRadius(0).outerRadius(r),
        count = 1,
        from = 0,
        to = 0;

    scale.domain([0, count - 1]).range([from, to]);

    layout.rotate(function () {
        return scale(~~(Math.random() * count));
    });

    tags = movieRank;
    layout.font("Impact").spiral("archimedean");
    fontSize = d3.scale["log"]().range([10, 100]);
    if (tags.length) fontSize.domain([+tags[tags.length - 1].value || 1, +tags[0].value]);
    layout.stop().words(tags).start();
})();
