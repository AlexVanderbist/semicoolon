<?php

namespace App\Http\Requests;

use App\Http\Requests\Request;

class UpdateProjectRequest extends Request
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize()
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules()
    {
        return [
            'name' => ['required'],
            'lat' => ['required'],
            'lng' => ['required'],
            'locationText' => ['required'],
            'stage_id' => ['required'],
            'thema_id' => ['required'],
            'project_creator' => ['required']
        ];
    }
}