/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.resize_enabled = false;
    config.resize_minWidth = 450;
    config.height = 400;
    config.skin = 'v2';
    config.removePlugins = 'save,forms';
    config.startupOutlineBlocks = true;
};
