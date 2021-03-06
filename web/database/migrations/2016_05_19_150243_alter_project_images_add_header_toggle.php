<?php

use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class AlterProjectImagesAddHeaderToggle extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::table('project_images', function (Blueprint $table) {
            $table->boolean('is_header');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('project_images', function (Blueprint $table) {
            $table->dropColumn('is_header');
        });
    }
}
