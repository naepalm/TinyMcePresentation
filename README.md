# TinyMcePresentation
Showing off the different features of TinyMce Premium in Umbraco

## Using the Demo

The site is built on Umbraco 13 and can be loaded and run without any need for front-end builds.

The database is hosted locally using SQLite, and the username/password should be:
admin@example.com
1234567890

When you load the site, there will be no data types, doctypes, or content. You will need to go to Settings and import everything through uSync. This should import all the settings, including the content :)

### AppSettings

You will need to have a [Tiny license](https://www.tiny.cloud/pricing/) (there is a free one), and if you wish to use the AI tool then you will also need the [AI API Key through OpenAi](https://platform.openai.com/api-keys). You can add these directly to the `TinyMceConfig` section in the appSettings.json or to your user secrets.

```
{
    "TinyMceConfig": {
        "apikey": "GET-FROM-TINY",
        "openAiApikey": "GET-FROM-OPEN-AI"
    }
}
```
