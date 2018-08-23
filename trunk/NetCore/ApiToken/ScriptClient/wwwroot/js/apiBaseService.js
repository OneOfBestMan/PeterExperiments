var apiBaseService = function () {

	var _getToken = function (authurl, username, password) {
		var input = {
			Username: username,
			Password: password
		};
		$.ajax({
			url: authurl,
			dataType: 'json',
			type: 'post',
			crossDomain: true,
			contentType: 'application/json',
			data: JSON.stringify(input),
			success: function (result) {
				console.log(result);
				var token = result.data.token;
				localStorage.setItem("token", token);
				_setToken(token);
				return token;
			}
		});
	}

	var _setToken = function (token) {
		//设置HTTP头
		$.ajaxSetup({
			beforeSend: function (xhr) {
				if (token) {
					xhr.setRequestHeader('Authorization', 'Bearer ' + token);
				}
				else {
					xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem("token"));
				}
			}
		});
	}
	return {
		getToken: _getToken,
		setToken: _setToken
	};

}();