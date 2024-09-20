!(function () {

    function init() {

        window.tinymcepremium.Config.custom_user_config = {
            // Add your config here -- can contain javascript after the ":"
            // YEP, ITS A JAVASCRIPT CALLBACK
            advtemplate_list: () => advTemplatePromise,
        };

        let advTemplatePromise = fetch("/umbraco/api/AdvancedTemplates/GetTemplates", {
            method: 'GET',
            })
            .then((response) => response.json())
            .then((data) => data)
            .catch((error) => console.info('Failed to get template list\n' + error));

        // I can execute the promise in the console here, but
        // if I get it from the custom user config it does not appear to be
        // executing it and I'm not sure what I'm doing wrong
        console.info("Template List: ", advTemplatePromise);
    }

    /**
    * Initialize after the app.ready event 
    */
    angular.module("umbraco").run(function ($rootScope) {
        $rootScope.$on('app.ready', init)
    })

})()