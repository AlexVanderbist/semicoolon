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

    // GET Projects
	Route::get('projects', 'API\ProjectsController@index');
	Route::get('projects/{project}', 'API\ProjectsController@view');
	Route::get('projects/{project}/opinions', 'API\ProjectsController@opinions');

    // GET Themes
	Route::get('themes', 'API\ProjectsController@getThemes');

	// Authenticated users only
	Route::group(['middleware' => 'jwt.auth'], function() {

		// GET user
	    Route::get('authenticate/user', 'API\AuthenticateController@getAuthenticatedUser');

	    // GET Project proposals
	    Route::get('projects/{project}/proposals', 'API\ProposalsController@getProposals');

	    // GET Project proposals unanswered by user
	    Route::get('projects/{project}/proposals/user', 'API\ProposalsController@getProposalsForUser');

	    // POST Project proposals opinion by user
	    Route::post('proposals/{proposal}', 'API\ProposalsController@postProposalOpinionForUser');

		// POST Project opinion (comment) by user
		Route::post('projects/{project}/opinions', 'API\ProjectsController@postOpinion');

	});
});

/* Laravel front end */
Route::group(['middleware' => 'web'], function () {

	/* AUTH */
	Route::auth();


	/* Link to angular front-end */
	Route::get('/', function () {
		return view('angular.index');
	});


    /* FRONTEND */

	// Route::get('/', [
	// 	'as' => 'frontend.projects.map',
	// 	'uses' => 'Frontend\ProjectsController@map'
	// ]);

	// Route::get('projecten', [
	// 	'as' => 'frontend.projects.index',
	// 	'uses' => 'Frontend\ProjectsController@index'
	// ]);

	// Route::get('projecten/{project}', [
	// 	'as' => 'frontend.projects.info',
	// 	'uses' => 'Frontend\ProjectsController@info'
	// ]);

	// Route::post('projecten/{project}/react', [
	// 	'as' => 'frontend.projects.opinionstore',
	// 	'uses' => 'Frontend\ProjectsController@opinionstore'
	// ]);

	// Route::get('projecten/{project}/deletereaction/{opinion}', [
	// 	'as' => 'frontend.projects.opiniondestroy',
	// 	'uses' => 'Frontend\ProjectsController@opiniondestroy'
	// ]);



	/* BACKEND */
	Route::group(['middleware' => 'admin', 'prefix' => 'backend'], function() {

		Route::get('/', [
			'as' => 'backend.dashboard',
			'uses' => 'Backend\DashboardController@index'
		]);

		Route::resource('users', 'Backend\UsersController', ['except' => ['show']]);
		Route::get('users/{users}/confirm', [
			'as' => 'backend.users.confirm',
			'uses' => 'Backend\UsersController@confirm'
		]);

		Route::resource('themes', 'Backend\ThemesController', ['except' => ['show']]);
		Route::get('themes/{themes}/confirm', [
			'as' => 'backend.themes.confirm',
			'uses' => 'Backend\ThemesController@confirm'
		]);

		Route::resource('projects', 'Backend\ProjectsController', ['except' => ['show']]);
		Route::get('projects/{project}/confirm', [
			'as' => 'backend.projects.confirm',
			'uses' => 'Backend\ProjectsController@confirm'
		]);

		// Proposals
		Route::resource('projects/{project}/proposals', 'Backend\ProposalsController', ['except' => ['show', 'create', 'update', 'edit']]);
		Route::delete('projects/{project}/proposals/{proposal}/opinions', [
			'uses' => 'Backend\ProposalsController@destroyOpinions',
			'as' => 'backend.projects.{project}.proposals.opinions.destroy'
		]);

		// Images
		Route::resource('projects/{project}/images', 'Backend\ProjectImagesController', ['except' => ['show', 'edit', 'create']]);

		// Stages
		Route::resource('projects/{project}/stages', 'Backend\StagesController', ['except' => ['show', 'create', 'update', 'edit', 'show']]);
		Route::get('projects/{project}/stages/{stage}/edit', [
			'as' => 'backend.projects.{project}.stages.edit',
			'uses' => 'Backend\StagesController@edit'
		]);
		Route::put('projects/{project}/stages/{stage}/update', [
			'as' => 'backend.projects.{project}.stages.update',
			'uses' => 'Backend\StagesController@update'
		]);
	});

});
