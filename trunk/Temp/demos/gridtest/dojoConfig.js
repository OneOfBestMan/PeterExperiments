var dojoConfig;
(function () {
	var baseUrl = location.pathname.replace(/\/[^/]+$/, '/../../../../js/');
	dojoConfig = {
		async: 1,
		cacheBust: '20170512',
		// Load dgrid and its dependencies from a local copy.
		// If we were loading everything locally, this would not
		// be necessary, since Dojo would automatically pick up
		// dgrid as a sibling of the dojo folder.
		packages: [
			{ name: 'dgrid', location: baseUrl + 'dgrid' },
			{ name: 'dstore', location: baseUrl + 'dstore' }
		]
	};
}());
