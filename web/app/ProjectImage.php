<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class ProjectImage extends Model
{
	protected $fillable = ['project_id', 'filename', 'description', 'is_header'];

    public function project() {
    	return $this->belongsTo(Project::class);
    }
}