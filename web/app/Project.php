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
        'project_creator',
        'youtube_url'
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

    public function proposals()
    {
        return $this->hasMany(Proposal::class);
    }

    public function youtubeID($url)
    {
        parse_str( parse_url( $url, PHP_URL_QUERY ), $id );
        return $id['v'];
    }
}
