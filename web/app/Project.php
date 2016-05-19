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

    protected $appends = ['header_image', 'youtube_id'];

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
        return $this->hasMany(Stage::class)->orderBy('startdate', 'asc');
    }

    public function proposals()
    {
        return $this->hasMany(Proposal::class);
    }

    public function images()
    {
        return $this->hasMany(ProjectImage::class);
    }

    public function opinions()
    {
        return $this->hasMany(Opinion::class);
    }

    public function setYoutubeUrlAttribute($value) {
        parse_str( parse_url( $value, PHP_URL_QUERY ), $id );
        $this->attributes['youtube_url'] = $id['v'];
    }

    public function getYoutubeUrlAttribute ($value) {
        return 'https://www.youtube.com/watch?v=' . $value;
    }

    public function getYoutubeIdAttribute () {
        return $this->attributes['youtube_url'];
    }

    public function getHeaderImageAttribute() {
        $image = $this->images()->where('is_header', 1)->first();
        if(!$image) $image = $this->images()->first();
        return $image;
    }
}
