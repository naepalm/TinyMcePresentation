!(function () {

    function init() {

        window.tinymcepremium.Config.custom_user_config = {
            // Add your config here -- can contain javascript after the ":"
            // YEP, ITS A JAVASCRIPT CALLBACK
            advtemplate_list: () =>
                fetch("/umbraco/api/AdvancedTemplates/GetTemplates", {
                    method: 'GET',
                })
                .then((response) => response.json())
                .then((data) => data)
                .catch((error) => console.info('Failed to get template list\n' + error)),

        };
    }

    /**
    * Initialize after the app.ready event 
    */
    angular.module("umbraco").run(function ($rootScope) {
        $rootScope.$on('app.ready', init)
    })

})()