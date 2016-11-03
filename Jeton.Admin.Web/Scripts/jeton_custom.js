function BindAppStatusChart(elementId, apiUrl) {
    $.getJSON(apiUrl,
        function (d) {
            var activeRootAppCount = $.grep(d,
                    function (n, i) {
                        return n.IsRoot === true && (n.IsDeleted === null || n.IsDeleted === false);
                    })
                .length;

            var activeNormalAppCount = $.grep(d,
                    function (n, i) {
                        return n.IsRoot === false && (n.IsDeleted === null || n.IsDeleted === false);
                    })
                .length;

            var inactiveRootAppCount = $.grep(d,
               function (n, i) {
                   return n.IsRoot === true && n.IsDeleted === true;
               })
           .length;

            var inactiveNormalAppCount = $.grep(d,
                    function (n, i) {
                        return n.IsRoot === false && n.IsDeleted === true;
                    })
                .length;

         

            Morris.Donut({
                element: elementId,
                data: [
                    {
                        label: "Active Root Apps",
                        value: activeRootAppCount
                    }, {
                        label: "Active Normal Apps",
                        value: activeNormalAppCount
                    }, {
                        label: "Inactive Root Apps",
                        value: inactiveRootAppCount
                    }, {
                        label: "Inactive Normal Apps",
                        value: inactiveNormalAppCount
                    }
                ],
                resize: true,
                colors: ["#5CB85C", "#337AB7", "#D9534F", "#F0AD4E"]
            });
        });
}

function BindTokenActivityChart(elementId, apiUrl) {
    $.getJSON(apiUrl, function (d) {
        Morris.Area({
            element: elementId,
            data: [{
                period: '2010 Q1',
                iphone: 2666,
                ipad: null,
                itouch: 2647
            }, {
                period: '2010 Q2',
                iphone: 2778,
                ipad: 2294,
                itouch: 2441
            }, {
                period: '2010 Q3',
                iphone: 4912,
                ipad: 1969,
                itouch: 2501
            }, {
                period: '2010 Q4',
                iphone: 3767,
                ipad: 3597,
                itouch: 5689
            }, {
                period: '2011 Q1',
                iphone: 6810,
                ipad: 1914,
                itouch: 2293
            }, {
                period: '2011 Q2',
                iphone: 5670,
                ipad: 4293,
                itouch: 1881
            }, {
                period: '2011 Q3',
                iphone: 4820,
                ipad: 3795,
                itouch: 1588
            }, {
                period: '2011 Q4',
                iphone: 15073,
                ipad: 5967,
                itouch: 5175
            }, {
                period: '2012 Q1',
                iphone: 10687,
                ipad: 4460,
                itouch: 2028
            }, {
                period: '2012 Q2',
                iphone: 8432,
                ipad: 5713,
                itouch: 1791
            }],
            xkey: 'period',
            ykeys: ['iphone', 'ipad', 'itouch'],
            labels: ['iPhone', 'iPad', 'iPod Touch'],
            pointSize: 2,
            hideHover: 'auto',
            resize: true
        });
    });
}

function BindAppActivityBarChart(elementId, apiUrl) {
    $.getJSON(apiUrl,
        function (d) {
            Morris.Bar({
                element: elementId,
                data: [{
                    y: '2006',
                    a: 100,
                    b: 90
                }, {
                    y: '2007',
                    a: 75,
                    b: 65
                }, {
                    y: '2008',
                    a: 50,
                    b: 40
                }, {
                    y: '2009',
                    a: 75,
                    b: 65
                }, {
                    y: '2010',
                    a: 50,
                    b: 40
                }, {
                    y: '2011',
                    a: 75,
                    b: 65
                }, {
                    y: '2012',
                    a: 100,
                    b: 90
                }],
                xkey: 'y',
                ykeys: ['a', 'b'],
                labels: ['Series A', 'Series B'],
                hideHover: 'auto',
                resize: true
            });
        });
}