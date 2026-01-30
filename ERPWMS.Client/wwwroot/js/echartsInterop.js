
window.echartsInterop = {
    initChart: function (elementId, option) {
        var chartDom = document.getElementById(elementId);
        if (!chartDom) return;
        var myChart = echarts.init(chartDom);
        myChart.setOption(option);
        
        window.addEventListener('resize', function() {
            myChart.resize();
        });
    },
    updateChart: function (elementId, option) {
        var chartDom = document.getElementById(elementId);
        if (chartDom) {
            var myChart = echarts.getInstanceByDom(chartDom);
            if (myChart) {
                myChart.setOption(option);
            }
        }
    }
};
