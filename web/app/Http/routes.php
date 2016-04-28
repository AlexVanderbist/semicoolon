<?php

/*
|--------------------------------------------------------------------------
| Routes File
|--------------------------------------------------------------------------
|
| Here is where you will register all of the routes in an application.
| It's a breeze. Simply tell Laravel the URIs it should respond to
| and give it the controller to call when that URI is requested.
|
*/

/* API */
Route::group(['prefix' => 'api/v1', 'middleware' => 'api'], function()
{
	Route::post('authenticate', 'API\AuthenticateController@authenticate');
    Route::get('authenticate/user', 'API\AuthenticateController@getAuthenticatedUser');
});

Route::group(['middleware' => 'web'], function () {
    
    /* AUTH */
    Route::auth();


    /* FRONTEND */
    Route::get('/', 'HomeController@index');

	Route::get('projecten', [
		'as' => 'frontend.projects.index',
		'uses' => 'Frontend\ProjectsController@index'
	]);

	Route::get('projecten/{project}', [
		'as' => 'frontend.projects.info',
		'uses' => 'Frontend\ProjectsController@info'
	]);


	/* BACKEND */
	Route::get('/backend', [
		'as' => 'backend.dashboard',
		'uses' => 'Backend\DashboardController@index'
	]);

	Route::resource('backend/users', 'Backend\UsersController', ['except' => ['show']]);
	Route::get('backend/users/{users}/confirm', [
		'as' => 'backend.users.confirm',
		'uses' => 'Backend\UsersController@confirm'
	]);

	Route::resource('backend/themes', 'Backend\ThemesController', ['except' => ['show']]);
	Route::get('backend/themes/{themes}/confirm', [
		'as' => 'backend.themes.confirm',
		'uses' => 'Backend\ThemesController@confirm'
	]);

	Route::resource('backend/projects', 'Backend\ProjectsController', ['except' => ['show']]);
	Route::get('backend/projects/{project}/confirm', [
		'as' => 'backend.projects.confirm',
		'uses' => 'Backend\ProjectsController@confirm'
	]);
});
