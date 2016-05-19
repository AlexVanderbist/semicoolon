<?php

namespace App\Http\Controllers\Backend;

use Illuminate\Http\Request;
use App\Project;
use App\Proposal;
use App\Http\Requests;

class ProposalsController extends Controller
{
    protected $proposals;

    public function __construct(Proposal $proposals) {
        $this->proposals = $proposals;

        parent::__construct();
    }

    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index(Project $project, Proposal $proposal)
    { 
        $proposals = $project->proposals;

        return view('backend.projects.proposals.index', compact('project', 'proposals', 'proposal'));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Requests\StoreProjectProposalRequest $request, Project $project)
    {
        $this->proposals->create($request->all() + ['project_id' => $project->id]);

        return redirect(route('backend.projects.{project}.proposals.index', $project->id))->with('stats', 'De stelling is toegevoegd!');
    }

    public function destroyOpinions(Project $project, $id)
    {

        $proposal = $this->proposals->findOrFail($id);

        $proposal->opinions()->delete();

        return redirect(route('backend.projects.{project}.proposals.index', $project->id))->with('status', 'De stelling is reset.');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy(Project $project, $id)
    {

        $proposal = $this->proposals->findOrFail($id);

        $proposal->delete();

        return redirect(route('backend.projects.{project}.proposals.index', $project->id))->with('status', 'De stelling is verwijderd.');
    }
}
