var quietMode = false;
var filesLoaded = 0;

$(document).ready(function () {

    $('.formPhotoUpload .btnImgUpload').on('click', function () {
        $("#loading").show();
        $("#imageProcessing").show();
        $("#imageLoading").hide();

        //upload image
        var source = document.getElementById('selector1').file;

        if ((source == null || source.length < 1000)) {
            if (!quietMode)
                alert("Укажите файл фотографии!");
            $("#loading").hide();
            return false;
        }

        var imageToResize = new Image();
        imageToResize.src = source;

        var maxSizeInput = $('.MaxImageSizeHidden:first');
        var maxSize = parseInt(maxSizeInput.val(), 10);
        var currentSize = source.length;

        var resultImage = source.replace("image/jpeg", "image/octet-stream");

        $("#imageProcessing").hide();
        $("#imageLoading").show();

        var formDataJSON = [resultImage.toString()];

        $.ajax({
            type: "POST",
            url: $(this).data('url'),
            data: JSON.stringify(formDataJSON),
            dataType: 'json',
            async: false,
            contentType: 'application/json; charset=utf-8',
            processData: false,
            beforeSend: function () {

            },
            success: function (data, event) {
                if (data.isUploaded) {
                    $('.photoUploadResult1').show();
                    $('.formPhotoUpload1').hide();

                    filesLoaded++;
                }
                else {

                }
                $("#loading").hide();

                if (!quietMode)
                    alert(data.message);
            },
            error: function (event, data) {
                if (data.files && data.files[0].error) {
                    if (!quietMode)
                        alert(data.files[0].error);
                }
                $("#loading").hide();
            }
        });
    });

    function check_file(filename, filetype) {
        str = filename.toUpperCase();
        suffix = ".JPG";
        suffix2 = ".JPEG";
        if (str.indexOf(suffix, str.length - suffix.length) == -1
			&& str.indexOf(suffix2, str.length - suffix2.length) == -1)
            return false;
        return true;
    }

    function get_approximate_quality(w, h, reqSize) {
        var appArray = [
			[1228800, [
				[50, 200],
				[60, 250],
				[70, 300],
				[80, 400],
				[90, 650],
				[95, 900]
			]],
			[2969600, [
				[50, 285],
				[60, 315],
				[70, 370],
				[80, 440],
				[90, 620],
				[95, 1000]
			]],
			[5017600, [
				[50, 310],
				[60, 430],
				[70, 520],
				[80, 570],
				[90, 770],
				[95, 1200]
			]],
			[8192000, [
				[20, 270],
				[30, 340],
				[40, 410],
				[50, 480],
				[60, 545],
				[70, 660],
				[80, 865],
				[90, 1500],
				[95, 2400]
			]],
			[10240000, [
				[20, 305],
				[30, 387],
				[40, 469],
				[50, 577],
				[60, 655],
				[70, 850],
				[80, 1200],
				[90, 1800],
				[95, 2700]
			]],
			[15360000, [
				[10, 340],
				[15, 390],
				[20, 450],
				[30, 580],
				[40, 820],
				[50, 920],
				[60, 1000],
				[70, 1300],
				[80, 1800],
				[90, 2900],
				[95, 4000]
			]],
			[18432000, [
				[10, 426],
				[15, 538],
				[20, 570],
				[30, 902],
				[40, 975],
				[50, 1010],
				[60, 1150],
				[70, 1300],
				[80, 1700],
				[90, 2500],
				[95, 3500]
			]],
			[23552000, [
				[10, 544],
				[15, 630],
				[20, 714],
				[30, 880],
				[40, 930],
				[50, 1100],
				[60, 1300],
				[70, 1400],
				[80, 1500],
				[90, 1900],
				[95, 3200]
			]]
		];

        var pixels = w * h;
        var resolutionsCount = appArray.length;
        var i, j, k;
        var nearestResIndex = 0;
        var nearestResDist = Math.abs(appArray[0][0] - pixels);
        for (i = 1; i < resolutionsCount; i++) {
            if (Math.abs(appArray[i][0] - pixels) < nearestResDist) {
                nearestResIndex = i;
                nearestResDist = Math.abs(appArray[i][0] - pixels);
            }
        }

        var qualitiesCount = appArray[nearestResIndex][1].length;
        var nearestQualIndex = 0;
        var nearestQualDist = Math.abs(appArray[nearestResIndex][1][0][1] - reqSize);
        for (i = 1; i < qualitiesCount; i++) {
            if (Math.abs(appArray[nearestResIndex][1][i][1] * 1024 - reqSize) < nearestQualDist) {
                nearestQualIndex = i;
                nearestQualDist = Math.abs(appArray[nearestResIndex][1][i][1] * 1024 - reqSize);
            }
        }

        var nearestQuality = appArray[nearestResIndex][1][nearestQualIndex][0];
        k = 1;
        if (appArray[nearestResIndex][1][nearestQualIndex][1] * 1024 > reqSize)
            k = appArray[nearestResIndex][1][nearestQualIndex][1] * 1024 / reqSize;

        var zapas = 1;

        return (nearestQuality / (k * zapas)).toFixed(0)
        //return 75;
    }

});
