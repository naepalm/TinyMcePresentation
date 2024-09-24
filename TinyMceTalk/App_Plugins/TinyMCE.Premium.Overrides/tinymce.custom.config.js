!(function () {

    function init($http, umbRequestHelper) {

        window.tinymcepremium.Config.custom_user_config = {
            // To update the advanced templates, you have to set the default to null
            advtemplate_templates: null,

            // You can get the entire list from an API response response
            advtemplate_list: () => umbRequestHelper.resourcePromise(
                    $http.get('/umbraco/api/AdvancedTemplates/GetTemplates'),
                    'Failed to get template list'
                ).then((data) => data),

            // You can get a single item from an API response
            advtemplate_get_template: (id) => umbRequestHelper.resourcePromise(
                    $http.get('/umbraco/api/AdvancedTemplates/GetTemplate/?id=' + id),
                    'Failed to get template list'
                )
                .then(({ id, title, content }) => ({ id, title, content }))
        };
    }

    /**
    * Initialize after the app.ready event 
    */
    angular.module("umbraco").run(function ($rootScope, $http, umbRequestHelper) {
        $rootScope.$on('app.ready', init($http, umbRequestHelper))
    })

})()