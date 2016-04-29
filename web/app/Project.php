<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Project extends Model
{
    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'name', 
        'description',
        'lat',
        'lng',
        'radius',
        'locationText',
        'theme_id',
        'project_creator'
    ];

    public function creator()
    {
        return $this->belongsTo(User::class, 'project_creator');
    }

    public function theme()
    {
        return $this->belongsTo(Theme::class);
    }

    public function stages()
    {
        return $this->hasMany(Stage::class);
    }


}
