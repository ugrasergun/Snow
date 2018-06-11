$(document).ready(function () {

    $('#btnUploadFile').on('click', function () {

        var data = new FormData();

        var files = $("#fileUpload").get(0).files;

        if (files.length > 0) {
            data.append("UploadedFile", files[0]);
        }

        $.ajax({
            type: "POST",
            url: "/api/snow/uploadfile",
            contentType: false,
            processData: false,
            dataType: "json",
            data: data,
            success: function (response) {
                
                var ctx = canvas.getContext('2d');
                var labels = response.map(function (e) {
                    return e.Name;
                });
                var data = response.map(function (e) {
                    return e.Value;
                });;
                var backgroundColor = response.map(function (e) {
                    return e.Colour;
                });; 

                var config = {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Snow Bars',
                            data: data,
                            backgroundColor: backgroundColor
                        }]
                    },
                    options: {
                        responsive: false,
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true
                                }
                            }]
                        }
                    }
                };
                var chart = new Chart(ctx, config);
            }
        });        
    });
});

