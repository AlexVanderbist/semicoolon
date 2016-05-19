<?php

namespace App\Http\Controllers\Backend;

use Illuminate\Http\Request;
use File;

use App\Http\Requests;

use App\ProjectImage;
use App\Project;

class ProjectImagesController extends Controller
{
    protected $projects;
    protected $projectImages;

    public function __construct(Project $projects, ProjectImage $projectImages) {
        $this->projects = $projects;
        $this->projectImages = $projectImages;
    }

    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index($projectId, ProjectImage $projectImage)
    {
        $project = $this->projects->find($projectId)->with('images')->first();
        return view('backend.projects.images.index', compact('project', 'projectImage'));
    }

    public function store(Requests\UploadPhotosRequest $request, Project $project) {

        // getting all of the post data
        $files = $request->file('images');

        // Making counting of uploaded images
        $file_count = count($files);

        // start count how many uploaded
        $uploadcount = 0;

        foreach($files as $file) {
            $destinationPath = 'photos/uploads';
            $filename = $file->getClientOriginalName();
            $upload_success = $file->move(public_path($destinationPath), $filename);
            $project->images()->create([
                'filename' => $destinationPath . '/' . $filename,
                'is_header' => false
            ]);
            $uploadcount++;
        }

        return redirect(route('backend.projects.{project}.images.index',$project->id))->with('status', $uploadcount . '/' . $file_count . ' foto\'s zijn geüpload.');
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function update(Project $project, $imageId)
    {
        $project->images()->update(['is_header' => false]);

        $this->projectImages->findOrFail($imageId)->update(['is_header' => true]);

        return redirect(route('backend.projects.{project}.images.index', $project->id))->with('status', 'Foto is ingesteld als hoofdafbeelding.');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy(Project $project, $imageId)
    {
        $image = $this->projectImages->findOrFail($imageId);

        File::delete(public_path($image->filename));

        $image->delete();

        return redirect(route('backend.projects.{project}.images.index', $project->id))->with('status', 'Foto verwijderd.');
    }
}
