var color = d3.scale.linear().domain([1, d3.max(zipCount, function (d) { return d.value; })]).range(['#7777AA', '#2222FF']);
stateColors = {};
stateData = {}
zipCount.forEach(function (d) {
    stateColors[d.name] = color(d.value)
    stateData[d.name] = { "users": d.value }
})
var map = new Datamap({
    element: document.getElementById('stateCount'),
    scope: 'usa',
    fills: {
        defaultFill: 'grey'
    },
    data: stateData,
    geographyConfig: {
        popupTemplate: function (geo, data) {
            if (data == null) {
                data = { "users": 0 }
            }
            return ['<div class="hoverinfo"><strong>',
                    geo.properties.name + '<br>',
                    'Users: ' + data.users,
                    '</strong></div>'].join('');
        }
    }
});
map.updateChoropleth(stateColors);
