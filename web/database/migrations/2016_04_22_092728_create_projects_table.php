<?php

use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateProjectsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('projects', function (Blueprint $table) {
            $table->increments('id');
            $table->string('name');
            $table->double('lat', 15, 13);
            $table->double('lng', 15, 13);
            $table->string('locationText');
            $table->integer('stage_id')->unsigned();
            $table->integer('thema_id')->unsigned();
            $table->integer('project_creator')->unsigned();
            $table->timestamps();

            $table->foreign('stage_id')->references('id')->on('stages')->onDelete('cascade');
            $table->foreign('thema_id')->references('id')->on('themes')->onDelete('cascade');
            $table->foreign('project_creator')->references('id')->on('users')->onDelete('cascade');
            
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::drop('projects');
    }
}
