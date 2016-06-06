<?php

namespace App\Http\Controllers\API;

use Illuminate\Http\Request;

use App\Http\Requests;
use App\Http\Controllers\Controller;

use App\Project;
use App\Proposal;
use App\Opinion;
use App\Theme;
use App\UserNotification;

use Auth;

class UserController extends Controller
{
    public function postNotification (Request $request, Project $project) {
    	// maybe check if the project exists first?yolo
    	if($request->notifications == 1) {
	    	UserNotification::firstOrCreate([
	    		'user_id' => Auth::user()->id,
	    		'project_id' => $project->id
	    	]);
	    	$notification = true;
    	} else {
    		UserNotification::where('user_id', Auth::user()->id)->where('project_id', $project->id)->delete();
    		$notification = false;
    	}

    	return response()->json(['notificationStatus' => $notification]);
    }

    public function getNotification (Request $request, Project $project) {

    	$notification = UserNotification::where('user_id', Auth::user()->id)->where('project_id', $project->id)->exists();

    	return response()->json(['notificationStatus' => $notification]);
    }
}
