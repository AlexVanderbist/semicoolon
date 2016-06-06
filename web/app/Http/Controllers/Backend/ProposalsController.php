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

    public function export(Project $project, Proposal $proposal)
    {
        $proposals = Proposal::where('project_id', $project->id)
                              ->get(['description']);
        Excel::create('Statistieken ' . $project->name, function($excel) use ($proposals) {
            $excel->sheet('Sheet1', function($sheet) use ($proposals) {
                $sheet->fromArray($proposals, null, 'A2', true, false);
                $sheet->setAutoSize(true);
                $sheet->row(1, array(
                     'Stelling', 'Antwoorden', 'Aantal Antwoorden'
                ));
                $sheet->row(1, function($row) {
                    $row->setFontWeight('bold');
                });
            });

        })->download('csv');
        return redirect(route('backend.projects.{project}.proposals.index', $project->id))->with('status', 'De statistieken zijn opgeslaan.');
    }
}
