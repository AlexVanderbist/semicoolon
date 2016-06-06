<?php

namespace App\Http\Controllers\Backend;

use Illuminate\Http\Request;
use App\Project;
use App\Proposal;
use App\Http\Requests;
use Excel;
use DB;

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
        $typeNames = [
            1 => 'Ja/nee vraag',
            2 => 'Score 1-5'
        ];

        return view('backend.projects.proposals.index', compact('project', 'proposals', 'proposal', 'typeNames'));
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

    public function export(Project $project)
    {
        $dataArray = DB::table('proposals')
                        // ->join('proposal_opinions', 'proposals.id', '=', 'proposal_opinions.proposal_id')
                        // ->where('proposals.project_id', $project->id)
                        ->join('proposal_opinions', function ($join) {
                            $join->on('proposals.id', '=', 'proposal_opinions.proposal_id')
                                ->where('proposals.project_id', $project->id);
                        })
                        ->select('proposals.description', 'proposal_opinions.updated_at', 'proposal_opinions.value')
                        ->get();
        dd($dataArray);
        Excel::create('Statistieken ' . $project->name, function($excel) use ($project) {
            $excel->sheet($project->name, function($sheet) use ($project) {
                $sheet->setWidth(array(
                    'B'     =>  30,
                ));

                $sheet->fromArray($project->proposals);
            });

        })->download('csv');
        return redirect(route('backend.projects.{project}.proposals.index', $project->id))->with('status', 'De statistieken zijn opgeslaan.');
    }
}
