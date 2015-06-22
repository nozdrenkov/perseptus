$(window).load(function () {
	if (("FileReader" in window)) {
		$('.fileSelector').each(function (index) {
			$(this).append(
					'<p style="font-size: 1.1em; padding: 5px; border: 5px dashed #e67817;">'
					+ '<input type="file" id="fileSelectorInput' + $(this).attr('id') + '"'
					+ ' accept=".jpeg, .jpg"'
					+ ' style="width: 0;height: 0; display:none;">'
					+ '<img class="selImage' + $(this).attr('id') + '" style="display:none; max-width:280px;" src="#"/>'
					+ '<label for="fileSelectorInput' + $(this).attr('id')
					+ '" style="display: block; width: 300px; height: 50px;">'
					+ 'Нажмите здесь или перетащите изображение</label></p>');
		});
		window._readFileFromSelector = function (holder, files) {
			if (files && files[0]) {
				var imgType = files[0].type;
				imgType = imgType.toUpperCase();
				if (imgType.indexOf('JPEG', imgType.length - 'JPEG'.length) != -1
					|| imgType.indexOf('JPG', imgType.length - 'JPG'.length) != -1) {

					var imgSize = (files[0].size / 1024).toFixed(0);
					//$('label', holder).html(files[0].name + '<br/>' + imgSize + ' Кб');

					reader = new FileReader();
					reader.onload = function (event) {
						holder.file = event.target.result;
						$('img', holder).attr('src', event.target.result);
						$('img', holder).show();
					}
					reader.readAsDataURL(files[0]);
				}
			}
		};
		$('.fileSelector input')
			.change(function (event) {
				var holder = $(this).parents('.fileSelector')[0];
				var files = this.files;
				_readFileFromSelector(holder, files);
			});
		$('.fileSelector')
			.on("dragover", function (event) {
				event.preventDefault();
				$('p', this).addClass("dragHolderHover");
			})
			.on("dragleave", function (event) {
				event.preventDefault();
				$('p', this).removeClass("dragHolderHover");
			})
			.on("drop", function (event) {
				event.preventDefault();
				$('p', this).removeClass("dragHolderHover");

				var holder = this;
				var files = (event.originalEvent.files || event.originalEvent.dataTransfer.files);
				_readFileFromSelector(holder, files);
			});
	}
	else {
		if ((typeof Silverlight == 'undefined') || !Silverlight || !Silverlight.isInstalled()) {
			return;
		}

		/*var path = (function () {
		var s = document.getElementsByTagName('script'),
		p = s[s.length - 1];
		return (p.src ? p.src : p.getAttribute('src')).match(/(.*\/)/)[0] || "";
		})();*/
	}

	window.onFileSelectCallback = function (content, id) {
		var name = content[0].split(',')[0],
		base64 = content[0].split(',')[1],
		mime = { png: "image/png",
			jpg: "image/jpeg",
			jpeg: "image/jpeg",
			gif: "image/gif"
		}[name.match(/[^\.]*$/)[0]] || "";

		var file = { name: name, size: base64.length, data: base64, type: mime };
		document.getElementById(id).file = "data:" + file.type + ";base64," + file.data;
	};

	window.onSizeChangedCallback = function (width, height, id, parentId) {
		$('#' + parentId).width(width);
		$('#' + parentId).height(height);
		$('#' + id).width(width);
		$('#' + id).height(height);
	};
});