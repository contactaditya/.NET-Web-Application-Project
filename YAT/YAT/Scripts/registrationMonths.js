var parseDate = function (d) { return d3.time.format("%Y%m").parse(d3.time.format("%Y")(new Date()) + d) }
var margin = { top: 20, right: 20, bottom: 30, left: 50 },
    width = 960 - margin.left - margin.right,
    height = 500 - margin.top - margin.bottom;
//svg
var svg = d3.select("#registrationMonths").append("svg")
    .attr("width", width + margin.left + margin.right)
    .attr("height", height + margin.top + margin.bottom)
  .append("g")
    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
//axis
var x = d3.time.scale().range([0, width]);
var y = d3.scale.linear().range([height, 0]);
x.domain(d3.extent(registrationMonths, function (d) { return parseDate(d.name); }));
y.domain(d3.extent(registrationMonths, function (d) { return d.value; }));

var xAxis = d3.svg.axis().scale(x).orient("bottom");
var yAxis = d3.svg.axis().scale(y).orient("left");
svg.append("g").attr("class", "x axis").attr("transform", "translate(0," + height + ")").call(xAxis);
svg.append("g").attr("class", "y axis").call(yAxis)
    .append("text")
      .attr("transform", "rotate(-90)")
      .attr("y", 6)
      .attr("dy", ".71em")
      .style("text-anchor", "end")
      .text("Users");
//cleanup dates
registrationMonths.forEach(function (d) {
    d.name = parseDate(d.name);
});
//line
var line = d3.svg.line().x(function (d) { return x(d.name); }).y(function (d) { return y(d.value); });
svg.append("path").datum(registrationMonths).attr("class", "line").attr("d", line);