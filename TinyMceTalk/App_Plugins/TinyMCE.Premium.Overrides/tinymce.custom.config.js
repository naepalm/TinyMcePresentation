!(function () {

    function init($http, umbRequestHelper) {
        const userRequest = {};
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
                .then(({ id, title, content }) => ({ id, title, content })),
        };
    }

    /**
    * Initialize after the app.ready event 
    */
    angular.module("umbraco").run(function ($rootScope, $http, umbRequestHelper) {
        $rootScope.$on('app.ready', init($http, umbRequestHelper))
    })

})()





















//mentions_fetch: (query, success) => {

//    umbRequestHelper.resourcePromise($http.get('/umbraco/api/Mentions/GetMembers?query=' + query.term),
//        'Failed to get mentions list'
//    ).then((users) => {
//        // Where the user object must contain the properties `id` and `name`
//        // but you could additionally include anything else you deem useful.
//        success(users);
//    });
//},
//    mentions_menu_hover: (userInfo, success) => {
//        // request more information about the user from the server and cache it locally
//        if (!userRequest[userInfo.id]) {
//            userRequest[userInfo.id] = umbRequestHelper.resourcePromise($http.get('/umbraco/api/Mentions/GetMember?id=' + userInfo.id),
//                'Failed to get mentions list'
//            );
//        }
//        console.info("User Info: ", userRequest[userInfo.id]);
//        userRequest[userInfo.id].then((userDetail) => {
//            console.info("User Detail: ", userDetail);
//            const div = document.createElement('div');

//            div.innerHTML = (
//                '<div style="padding: 20px; background: white; border: solid 1px gray; box-shadow: 0 0 10px gray; max-width:300px;">' +
//                '<strong style="display: block; font-weight: bold;">' + userDetail.name + '</strong>' +
//                '<img src="' + userDetail.image + '" ' +
//                'style="width: 50px; height: 50px; float: left; margin-right: 10px;"/>' +
//                '<p>' + userDetail.description + '</p>' +
//                '</div>'
//            );

//            success(div);
//        });
//    }