data = ageRank;

margin = { top: 20, right: 20, bottom: 30, left: 40 },
width = 960 - margin.left - margin.right,
height = 500 - margin.top - margin.bottom;

x = d3.scale.ordinal().rangeRoundBands([0, width], .1);
y = d3.scale.linear().range([height, 0]);
xAxis = d3.svg.axis().scale(x).orient("bottom");
yAxis = d3.svg.axis().scale(y).orient("left").ticks(10);

//svg size
svg = d3.select("#ageRank").append("svg")
    .attr("width", width + margin.left + margin.right)
    .attr("height", height + margin.top + margin.bottom)
  .append("g")
    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

//values
x.domain(data.map(function (d) { return d.name; }));
y.domain([0, d3.max(data, function (d) { return d.value; })]);

//axis labels
svg.append("g").attr("class", "x axis").attr("transform", "translate(0," + height + ")").call(xAxis);
svg.append("g").attr("class", "y axis").call(yAxis)
    .append("text")
       .attr("transform", "rotate(-90)")
       .attr("y", 6)
       .attr("dy", ".71em")
       .style("text-anchor", "end")

//bar size
svg.selectAll(".bar").data(data).enter().append("rect")
    .attr("class", "bar")
    .attr("x", function (d) { return x(d.name); })
    .attr("width", x.rangeBand())
    .attr("y", function (d) { return y(d.value); })
    .attr("height", function (d) { return height - y(d.value); });