width = 400
height = 400
radius = 200
colors = d3.scale.category20c()

pie = d3.layout.pie()
         .value(function (d) {
             return d.value
         })
arc = d3.svg.arc().outerRadius(radius)

//svg size
d3.select("#genderCount")
    .append('svg').attr('width', width).attr('height', height)
    .append('g').attr('transform', 'translate(' + (width - radius) + ',' + (height - radius) + ')')
    .selectAll('path').data(pie(genderCount)).enter()
        .append('g').attr('class', 'slice')
//slice size
d3.selectAll('g.slice').append('path')
   .attr('fill', function (d, i) {
       return colors(i)
   })
   .attr('d', arc)
//slice text
d3.selectAll('g.slice').append('text')
  .text(function (d, i) {
      if (d.data.name) {
          return "Male"
      }
      else {
          return "Female"
      }
  })
       .attr('text-anchor', 'middle')
       .attr('fill', 'white')
       .attr('transform', function (d) {
           d.innerRadius = 0;
           d.outerRadius = radius;
           return 'translate(' + arc.centroid(d) + ')'
       })
