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
	// Authenticate
	Route::post('authenticate', 'API\AuthenticateController@authenticate');

	// Authenticated users only
	Route::group(['middleware' => 'jwt.auth'], function() {

		// GET user
	    Route::get('authenticate/user', 'API\AuthenticateController@getAuthenticatedUser');

	    // GET Projects
	    Route::get('projects', 'API\ProjectsController@index');

	    // GET Project proposals
	    Route::get('projects/{project}/proposals', 'API\ProjectsController@getProposals');

	});
});

Route::group(['middleware' => 'web'], function () {
    
    /* AUTH */
    Route::auth();


    /* FRONTEND */

	Route::get('/', [
		'as' => 'frontend.projects.map',
		'uses' => 'Frontend\ProjectsController@map'
	]);

	Route::get('projecten', [
		'as' => 'frontend.projects.index',
		'uses' => 'Frontend\ProjectsController@index'
	]);

	Route::get('projecten/{project}', [
		'as' => 'frontend.projects.info',
		'uses' => 'Frontend\ProjectsController@info'
	]);

	Route::post('projecten/{project}/react', [
		'as' => 'frontend.projects.opinionstore',
		'uses' => 'Frontend\ProjectsController@opinionstore'
	]);

	Route::post('projecten/{project}/deletereaction', [
		'as' => 'frontend.projects.opiniondestroy',
		'uses' => 'Frontend\ProjectsController@opiniondestroy'
	]);



	/* BACKEND */ // add middleware around this instead of on the parent constructor?
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

	Route::resource('backend/projects/{project}/proposals', 'Backend\ProposalsController', ['except' => ['show', 'create', 'update', 'edit', 'show']]);

});
