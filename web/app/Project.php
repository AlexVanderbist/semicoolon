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

    public function setYoutubeUrlAttribute($value) {
        parse_str( parse_url( $value, PHP_URL_QUERY ), $id );
        $this->attributes['youtube_url'] = $id['v'];
    }

    public function getYoutubeUrlAttribute () {
        return 'https://www.youtube.com/watch?v=' . $this->youtube_url;
    }

    public function getYoutubeIdAttribute () {
        return $this->youtube_url;
    }

    public function youtubeID($url)
    {
        parse_str( parse_url( $url, PHP_URL_QUERY ), $id );
        return $id['v'];
    }
}
