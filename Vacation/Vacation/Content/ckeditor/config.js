/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
    config.filebrowserBrowseUrl = '~/~/Content/kcfinder/browse.php?opener=ckeditor&type=files';
    config.filebrowserImageBrowseUrl = '~/~/Content/kcfinder/browse.php?opener=ckeditor&type=images';
    config.filebrowserFlashBrowseUrl = '~/~/Content/kcfinder/browse.php?opener=ckeditor&type=flash';
    config.filebrowserUploadUrl = '~/~/Content/kcfinder/upload.php?opener=ckeditor&type=files';
    config.filebrowserImageUploadUrl = '~/~/Content/kcfinder/upload.php?opener=ckeditor&type=images';
    config.filebrowserFlashUploadUrl = '~/~/Content/kcfinder/upload.php?opener=ckeditor&type=flash';
};
