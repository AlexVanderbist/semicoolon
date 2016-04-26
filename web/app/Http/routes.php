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

Route::group(['middleware' => 'web'], function () {
    
    Route::auth();

    Route::get('/home', 'HomeController@index');

	Route::get('/', function () {
	    return view('welcome');
	});

	Route::resource('frontend/projects', 'Frontend\ProjectsController', ['except' => ['show']]);
	Route::get('frontend/projects/{projects}/confirm', [
		'as' => 'frontend.projects.confirm',
		'uses' => 'Frontend\ProjectsController@confirm'
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
});
