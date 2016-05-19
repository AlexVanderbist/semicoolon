<?php

namespace App\Http\Requests;

use App\Http\Requests\Request;

class UploadPhotosRequest extends Request
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
        $rules = [
        ];

        $nbr = count($this->input('images'));
        foreach(range(0, $nbr) as $index) {
            $rules['images.' . $index] = 'required|image';
        }

        return $rules;
    }
}
