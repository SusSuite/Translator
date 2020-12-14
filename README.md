# Translator

This is an Among Us Impostor plugin to translate player messages in real time. 

![Translator Example](/docs/Translator.png)

## Setup
Install SusSuite in your library folder.
Install Microsoft.Extensions in your library folder.
Install Translator in your plugins folder.

Run your server to generate the config folder:
> plugins/Tranlator/TranslatorSettings.json

Go ahead and stop your server.

You will need to add your Microsoft Cognitive Services TextTranslation Api Key to your settings.

Your endpoing should not need changed.

Your main language will be the short code for the language you want to traslate to.

TranslatorMode can be:
- 0 - To detect every message and translate non main language messages.
- 1 - The non language speaker will need to append /t to their message in order for it to be translated.
