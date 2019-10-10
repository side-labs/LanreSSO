// .vuepress/config.js
module.exports = {
    title: 'Lanre',
    description: 'Documentación del proyecto',
    themeConfig: {
        navbar: true,
        nav: [
            { text: 'Home', link: '/' },
            { text: 'Web app', link: '/webapp/' },
        ],
        sidebar:
        [
            {
                title: 'Web App',
                collapsable: false,
                children: [
                    ['/webapp/structure.md', 'Estructura'],
                    ['/webapp/swagger.md', 'Swagger'],

                ]
            },
        ]
    }
}