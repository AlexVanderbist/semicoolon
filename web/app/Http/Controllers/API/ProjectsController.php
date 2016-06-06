<?php

namespace App\Http\Controllers\API;

use Illuminate\Http\Request;

use App\Http\Requests;
use App\Http\Controllers\Controller;

use App\Project;
use App\Proposal;
use App\Opinion;
use App\Theme;

class ProjectsController extends Controller
{
	protected $projects;
    protected $proposals;
    protected $opinions;

	public function __construct(Project $projects, Proposal $proposals, Opinion $opinions) {

		$this->projects = $projects;
        $this->proposals = $proposals;
        $this->opinions = $opinions;
	}

    public function index()
    {
        $projects = $this->projects->with('theme', 'creator', 'images', 'stages')->get();
        //$projects = [];

        return response()->json(compact('projects'));
    }

    public function view($project)
    {
        $project = $this->projects->with('theme', 'creator', 'images', 'stages')->find($project);
        if(! $project) {
            return response()->json([
                'error' => 'Project not found',
            ], 404);
        }

        return response()->json(compact('project'));
    }

    public function opinions(Project $project)
    {
        $opinions = $project->opinions();
        if(! $opinions) {
            $opinions = [];
        } else {
            $opinions = $opinions->with('posted_by')->get();
        }

        return response()->json(compact('opinions'));
    }

    public function postOpinion(Requests\StoreOpinionRequest $request, Project $project)
    {
        $opinion = $this->opinions->create([
			'user_id' =>  auth()->user()->id,
			'project_id' => $project->id
			] + $request->only('opinion')
        );

		// return comments
		$opinions = $project->opinions()->with('posted_by')->get();
		return response()->json(compact('opinions'));
    }

    public function removeOpinion(Request $request, Opinion $opinion)
    {
        $opinion->delete();

		return response()->json(['status' => 'Opinion removed']);
    }

    public function destroyOpinion(Requests\DeleteOpinionRequest $request, Project $project, Opinion $opinion)
    {
        $opinion->delete();

		return response()->json(['status' => 'Opinion destroyed']);
    }

    public function getThemes(Theme $themes)
    {
    	$themes = $themes->all();
    	return response()->json(compact('themes'));
    }
}
